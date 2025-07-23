using System.Text.Json;
using System.Text.Json.Serialization;
using Evolution.Client.CSharp.Models.Instance;

namespace Evolution.Client.CSharp.Converters;

/// <summary>
/// Conversor JSON personalizado para o enum WhatsAppIntegration.
/// </summary>
public class WhatsAppIntegrationConverter : JsonConverter<WhatsAppIntegration>
{
    /// <summary>
    /// Lê o valor JSON e converte para o enum WhatsAppIntegration.
    /// </summary>
    /// <param name="reader">O leitor JSON.</param>
    /// <param name="typeToConvert">O tipo a ser convertido.</param>
    /// <param name="options">As opções de serialização.</param>
    /// <returns>O valor do enum WhatsAppIntegration.</returns>
    public override WhatsAppIntegration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "WHATSAPP-BAILEYS" => WhatsAppIntegration.WhatsAppBaileys,
            "WHATSAPP-BUSINESS" => WhatsAppIntegration.WhatsAppBusiness,
            _ => throw new JsonException($"Valor inválido para WhatsAppIntegration: {value}")
        };
    }

    /// <summary>
    /// Escreve o valor do enum WhatsAppIntegration como string JSON.
    /// </summary>
    /// <param name="writer">O escritor JSON.</param>
    /// <param name="value">O valor do enum a ser escrito.</param>
    /// <param name="options">As opções de serialização.</param>
    public override void Write(Utf8JsonWriter writer, WhatsAppIntegration value, JsonSerializerOptions options)
    {
        var stringValue = value switch
        {
            WhatsAppIntegration.WhatsAppBaileys => "WHATSAPP-BAILEYS",
            WhatsAppIntegration.WhatsAppBusiness => "WHATSAPP-BUSINESS",
            _ => throw new JsonException($"Valor inválido para WhatsAppIntegration: {value}")
        };
        
        writer.WriteStringValue(stringValue);
    }
}