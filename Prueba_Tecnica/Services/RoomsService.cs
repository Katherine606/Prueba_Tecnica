using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models.Entities;
using Prueba_Tecnica.Repositories;

namespace Prueba_Tecnica.Services
{
    public class RoomService
    {
        private readonly RoomsRepository _roomRepository;

        public RoomService(RoomsRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> ListarSalasAsync()
        {
            var salas = await _roomRepository.ListarSalasAsync();

            if (salas == null)
            {
                throw new ApiException("No hay salas registradas", 404);
            }

            return salas;
        }

        public async Task<Room> CrearSalasAsync(string name, int capacity, string location)
        {
            if (capacity <= 0)
            {
                throw new ApiException("La capacidad de la sala debe ser mayor a cero.",400);
            }

            var room = new Room
            {
                Name = name,
                Capacity = capacity,
                Location = location
            };

            await _roomRepository.CrearSalaAsync(room);
            await _roomRepository.SaveChangesAsync();
            return room;
        }
    }
}