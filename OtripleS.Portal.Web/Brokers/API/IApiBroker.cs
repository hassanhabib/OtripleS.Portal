// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial interface IApiBroker
    {
        ValueTask<T> GetAsync<T>(string relativeUrl);
        ValueTask<T> PostAsync<T>(string relativeUrl, T content);
        ValueTask<T> PutAsync<T>(string relativeUrl, T content);
        ValueTask<T> DeleteAsync<T>(string relativeUrl);
    }
}
