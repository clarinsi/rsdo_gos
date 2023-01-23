using Gos.ServiceModel.Enums;

namespace Gos.Web.Models;

public interface IResultsViewModel
{
    string Query { get; }

    TranscriptionType TranscriptionType { get; }
}