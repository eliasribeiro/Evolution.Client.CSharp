namespace Evolution.Client.Core.Http;

/// <summary>
/// Interface para serviços HTTP
/// </summary>
internal interface IHttpService
{
    /// <summary>
    /// Realiza uma requisição GET
    /// </summary>
    /// <typeparam name="TResponse">Tipo da resposta</typeparam>
    /// <param name="endpoint">Endpoint da API</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta deserializada</returns>
    Task<TResponse> GetAsync<TResponse>(
        string endpoint,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Realiza uma requisição POST com resposta
    /// </summary>
    /// <typeparam name="TRequest">Tipo da requisição</typeparam>
    /// <typeparam name="TResponse">Tipo da resposta</typeparam>
    /// <param name="endpoint">Endpoint da API</param>
    /// <param name="request">Dados da requisição</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta deserializada</returns>
    Task<TResponse> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Realiza uma requisição POST sem resposta
    /// </summary>
    /// <typeparam name="TRequest">Tipo da requisição</typeparam>
    /// <param name="endpoint">Endpoint da API</param>
    /// <param name="request">Dados da requisição</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task PostAsync<TRequest>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Realiza uma requisição PUT com resposta
    /// </summary>
    /// <typeparam name="TRequest">Tipo da requisição</typeparam>
    /// <typeparam name="TResponse">Tipo da resposta</typeparam>
    /// <param name="endpoint">Endpoint da API</param>
    /// <param name="request">Dados da requisição</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta deserializada</returns>
    Task<TResponse> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Realiza uma requisição PUT sem resposta
    /// </summary>
    /// <typeparam name="TRequest">Tipo da requisição</typeparam>
    /// <param name="endpoint">Endpoint da API</param>
    /// <param name="request">Dados da requisição</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task PutAsync<TRequest>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Realiza uma requisição DELETE com resposta
    /// </summary>
    /// <typeparam name="TResponse">Tipo da resposta</typeparam>
    /// <param name="endpoint">Endpoint da API</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta deserializada</returns>
    Task<TResponse> DeleteAsync<TResponse>(
        string endpoint,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Realiza uma requisição DELETE sem resposta
    /// </summary>
    /// <param name="endpoint">Endpoint da API</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task DeleteAsync(
        string endpoint,
        CancellationToken cancellationToken = default);
}
