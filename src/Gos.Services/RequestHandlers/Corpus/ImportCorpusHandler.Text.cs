﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Gos.Core;
using Gos.Core.Entities;
using Gos.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.RequestHandlers.Corpus
{
    public partial class ImportCorpusHandler
    {
        private async Task ImportDiscourse(XElement discourseEl)
        {
            var discourse = new Discourse();
            discourse.Code = discourseEl.Attribute(Constants.XmlNs + "id").Value;
            discourse.Statements = new List<Statement>();

            // Header
            var headerEl = discourseEl.Element(Constants.TeiNs + "teiHeader");
            await ImportHeader(headerEl, discourse);

            // Text
            var textEl = discourseEl.Element(Constants.TeiNs + "text");
            await ImportText(textEl, discourse);

            // Save all data for single discourse
            await dbContext.Discourses.AddAsync(discourse);
            await dbContext.SaveChangesAsync();

            // Remove tracked entities
            dbContext.ChangeTracker.Clear();
        }

        private async Task ImportTextFile(string sourcePath)
        {
            using (var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (var xmlReader = XmlReader.Create(stream))
                {
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "TEI" && xmlReader.NamespaceURI == Constants.TeiNamespace)
                        {
                            using (var discourseReader = xmlReader.ReadSubtree())
                            {
                                var discourseEl = XElement.Load(discourseReader);
                                await ImportDiscourse(discourseEl);
                            }
                        }
                    }
                }
            }
        }

        private async Task ImportText(XElement textEl, Discourse discourse)
        {
            var bodyEl = textEl.Element(Constants.TeiNs + "body");

            // Loop through statements
            var order = 0;
            var discourseTokenOrder = new DiscourseTokenOrder();
            foreach (var statementEl in bodyEl.Elements(Constants.TeiNs + "u"))
            {
                order++;
                await ImportStatement(discourse, statementEl, order, discourseTokenOrder);
            }
        }

        private async Task ImportStatement(Discourse discourse, XElement statementEl, int statementOrder, DiscourseTokenOrder discourseTokenOrder)
        {
            // Add statement
            var statement = new Statement
            {
                Code = statementEl.Attribute(Constants.XmlNs + "id").Value,
                Discourse = discourse,
                Order = statementOrder,
                Segments = new List<Segment>(),
                Speaker = await GetSpeaker(statementEl),
            };
            discourse.Statements.Add(statement);

            // Loop through segments
            var segmentOrder = 0;
            foreach (var segEl in statementEl.Elements(Constants.TeiNs + "seg"))
            {
                segmentOrder++;
                await ImportSegment(discourse, statement, segEl, segmentOrder, discourseTokenOrder);
            }
        }

        private async Task ImportSegment(Discourse discourse, Statement statement, XElement segEl, int segmentOrder, DiscourseTokenOrder discourseTokenOrder)
        {
            // Add segment
            var segment = new Segment()
            {
                Order = segmentOrder,
                Statement = statement,
                SoundFile = GetSoundFile(discourse, segEl),
                Tokens = new List<Token>(),
            };
            statement.Segments.Add(segment);

            // Get tokens
            var tokens = segmentParser.GetTokens(segEl).ToList();

            // apply marks and order tokens
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (token.Type == TokenType.Mark)
                {
                    // skip marks
                    continue;
                }

                if (i > 0 && tokens[i - 1].Type == TokenType.Mark)
                {
                    // left mark
                    token.LeftMark = tokens[i - 1].ConversationalForm;
                }

                if (i < tokens.Count - 1 && tokens[i + 1].Type == TokenType.Mark)
                {
                    // right mark
                    token.RightMark = tokens[i + 1].ConversationalForm;
                }

                discourseTokenOrder.Value += 1;
                token.DiscourseOrder = discourseTokenOrder.Value;
                token.Order = segment.Tokens.Count + 1;
                token.Segment = segment;
                segment.Tokens.Add(token);
            }
        }

        private async Task<Speaker> GetSpeaker(XElement statementEl)
        {
            var who = statementEl.Attribute("who")?.Value;
            if (string.IsNullOrEmpty(who))
            {
                return null;
            }

            var speakerCode = who.Substring(1);
            return await dbContext.Speakers.SingleAsync(s => s.Code == speakerCode);
        }

        private string GetSoundFile(Discourse discourse, XElement segEl)
        {
            var idAttr = segEl.Attribute(Constants.XmlNs + "id");
            if (idAttr == null || string.IsNullOrEmpty(idAttr.Value))
            {
                return null;
            }

            return discourse.Code + "/" + idAttr.Value + ".mp3";
        }

        private class DiscourseTokenOrder
        {
            public int Value { get; set; }
        }
    }
}