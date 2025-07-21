using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a resposta do endpoint fetch-instances da API Evolution (versão 2).
/// </summary>
public class InstancesResponseV2 : List<InstanceResponseV2>
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="InstancesResponseV2"/>.
    /// </summary>
    public InstancesResponseV2() : base()
    {
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="InstancesResponseV2"/> com uma coleção existente.
    /// </summary>
    /// <param name="collection">A coleção a ser copiada.</param>
    public InstancesResponseV2(IEnumerable<InstanceResponseV2> collection) : base(collection)
    {
    }
}