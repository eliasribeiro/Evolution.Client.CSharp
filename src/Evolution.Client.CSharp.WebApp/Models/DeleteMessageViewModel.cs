using System.ComponentModel.DataAnnotations;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para deletar mensagem para todos.
/// </summary>
public class DeleteMessageViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    public string? InstanceName { get; set; }

    /// <summary>
    /// ID da mensagem.
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// Remote JID.
    /// </summary>
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Indica se a mensagem é de mim.
    /// </summary>
    public bool FromMe { get; set; }

    /// <summary>
    /// Participante (usado em grupos).
    /// </summary>
    public string? Participant { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public DeleteMessageResult? Result { get; set; }
}

/// <summary>
/// Resultado da operação de deletar mensagem.
/// </summary>
public class DeleteMessageResult
{
    /// <summary>
    /// ID da mensagem.
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// Remote JID.
    /// </summary>
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Indica se a mensagem é de mim.
    /// </summary>
    public bool FromMe { get; set; }

    /// <summary>
    /// Timestamp da mensagem.
    /// </summary>
    public string? MessageTimestamp { get; set; }

    /// <summary>
    /// Participante (usado em grupos).
    /// </summary>
    public string? Participant { get; set; }

    /// <summary>
    /// Data/hora do timestamp como DateTime.
    /// </summary>
    public DateTime? TimestampAsDateTime
    {
        get
        {
            if (long.TryParse(MessageTimestamp, out var timestamp))
            {
                return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            }
            return null;
        }
    }
}