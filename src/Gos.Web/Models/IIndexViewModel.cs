using Gos.ServiceModel.Enums;

namespace Gos.Web.Models
{
    public interface IIndexViewModel
    {
        int Discourses { get; set; }

        int Statements { get; set; }

        int Words { get; set; }

        TranscriptionType TranscriptionType { get; }
    }
}