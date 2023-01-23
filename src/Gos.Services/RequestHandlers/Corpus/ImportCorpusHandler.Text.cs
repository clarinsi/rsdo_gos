using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            await dbContext.Discourses.AddAsync(discourse);

            // Header
            var headerEl = discourseEl.Element(Constants.TeiNs + "teiHeader");
            await ImportHeader(headerEl, discourse);

            // Text
            var textEl = discourseEl.Element(Constants.TeiNs + "text");
            await ImportText(textEl, discourse);

            // Save all data for single discourse
            await dbContext.SaveChangesAsync();
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
                Speaker = await GetSpeaker(statementEl),
            };
            await dbContext.Statements.AddAsync(statement);

            // Loop through segments
            var segmentOrder = 0;
            foreach (var segEl in statementEl.Elements(Constants.TeiNs + "seg"))
            {
                segmentOrder++;
                await ImportSegment(statement, segEl, segmentOrder, discourseTokenOrder);
            }
        }

        private async Task ImportSegment(Statement statement, XElement segEl, int segmentOrder, DiscourseTokenOrder discourseTokenOrder)
        {
            // Add segment
            var segment = new Segment()
            {
                Order = segmentOrder,
                Statement = statement,
                SoundFile = GetSoundFile(segEl),
            };
            await dbContext.Segments.AddAsync(segment);

            // Get tokens
            var tokens = segmentParser.GetTokens(segEl).ToList();

            // apply marks and order tokens
            var orderedTokens = new List<Token>();
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
                token.Order = orderedTokens.Count + 1;
                token.Segment = segment;
                orderedTokens.Add(token);
            }

            await dbContext.Tokens.AddRangeAsync(orderedTokens);
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

        private string GetSoundFile(XElement segEl)
        {
            var synchAttr = segEl.Attribute("synch");
            if (synchAttr == null || string.IsNullOrEmpty(synchAttr.Value))
            {
                return null;
            }

            return synchAttr.Value.Replace("#", "") + ".mp3";
        }

        private class DiscourseTokenOrder
        {
            public int Value { get; set; }
        }
    }
}