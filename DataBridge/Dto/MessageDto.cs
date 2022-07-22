using System.ComponentModel.DataAnnotations;

namespace DataBridge.Dto
{
    public record MessageDto([Required]string MessageToSend);
}
