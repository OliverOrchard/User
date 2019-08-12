using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace User.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string DatabaseId = "UserService";
        private const string CollectionId = "Users";

        private readonly IDocumentClient _documentClient;

        private static Uri CollectionUri => UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);

        public UserRepository(IDocumentClient documentClient)
        {
            _documentClient = documentClient;
        }

        public async Task<User.Domain.User> GetByIdAsync(string id)
        {
            try
            {
                return await _documentClient.ReadDocumentAsync<User.Domain.User>(
                    CreateDocumentUri(id),
                    new RequestOptions { PartitionKey = new PartitionKey(id) }
                );
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                    return null;

                throw;
            }
        }
        
        public async Task<bool> UpsertUser(User.Domain.User user)
        {
            var response = await _documentClient.UpsertDocumentAsync(CollectionUri, user,
                new RequestOptions { PartitionKey = new PartitionKey(user.Id) });

            return response.StatusCode == HttpStatusCode.OK;
        }

        private Uri CreateDocumentUri(string id)
        {
            return UriFactory.CreateDocumentUri(
                DatabaseId, CollectionId, id);
        }
    }
}
