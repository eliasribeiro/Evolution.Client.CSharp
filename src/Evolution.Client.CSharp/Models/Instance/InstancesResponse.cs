using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a resposta do endpoint fetch-instances da API Evolution (versão 2).
/// </summary>
public class InstancesResponse : List<InstanceResponse>
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="InstancesResponse"/>.
    /// </summary>
    public InstancesResponse() : base()
    {
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="InstancesResponse"/> com uma coleção existente.
    /// </summary>
    /// <param name="collection">A coleção a ser copiada.</param>
    public InstancesResponse(IEnumerable<InstanceResponse> collection) : base(collection)
    {
    }
}