using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;

namespace Dpay.Client.Processors.Interfaces
{
    /// <summary>
    /// ICardProcessor interface for card operations
    /// </summary>
    public interface ICardProcessor
    {
        /// <summary>
        /// This method is used to save Credit Card
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiUrl"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        SaveCardResponse SaveCard(SaveCardRequest request, string apiUrl, string secretKey);

        /// <summary>
        /// This method is used to delete Credit Card
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiUrl"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        DeleteCardResponse DeleteCard(DeleteCardRequest request, string apiUrl, string secretKey);

        /// <summary>
        /// This method is used to update Credit Card
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiUrl"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        UpdateCardResponse UpdateCard(UpdateCardRequest request, string apiUrl, string secretKey);

        /// <summary>
        /// This method is used to get card detail
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiUrl"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        GetMerchantCardResponse GetCard(GetMerchantCardRequest request, string apiUrl, string secretKey);
    }
}
