using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Model;

namespace PuntosApi.Services
{
    public class PointsService
    {
        private readonly IMongoCollection<Punto> _puntos;

        public PointsService(IPointsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _puntos = database.GetCollection<Punto>(settings.PointsCollectionName);
        }

        public List<Punto> Get() =>
            _puntos.Find(punto => true).ToList();

        public Punto Get(string id) =>
            _puntos.Find<Punto>(punto => punto.Id == id).FirstOrDefault();

        public Punto Create(Punto punto)
        {
            _puntos.InsertOne(punto);
            return punto;
        }

        public void Update(string id, Punto puntoIn) =>
            _puntos.ReplaceOne(punto => punto.Id == id, puntoIn);

        public void Remove(Punto puntoIn) =>
            _puntos.DeleteOne(punto => punto.Id == puntoIn.Id);

        public void Remove(string id) =>
            _puntos.DeleteOne(punto => punto.Id == id);
    }
}
