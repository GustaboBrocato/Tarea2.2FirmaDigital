using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea2._2FirmaDigital.Models;

namespace Tarea2._2FirmaDigital.Controllers
{
    public class firmaController
    {
        SQLiteAsyncConnection _connection;

        //Constructor vacio
        public firmaController() { }

        //Conexion a la base de datos
        public async Task Init()
        {
            try
            {
                if (_connection is null)
                {
                    SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;

                    if (string.IsNullOrEmpty(FileSystem.AppDataDirectory))
                    {
                        return;
                    }

                    _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBFirmas"), extensiones);

                    var creacion = await _connection.CreateTableAsync<Models.firmaDigital>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Init(): {ex.Message}");
            }
        }

        //Crear metodos crud para la clase personas
        //Create
        public async Task<int> storeFirma(firmaDigital firma)
        {
            await Init();
            if (firma.Id == 0)
            {
                return await _connection.InsertAsync(firma);
            }
            else
            {
                return await _connection.UpdateAsync(firma);
            }
        }

        //Update
        public async Task<int> updateFirma(firmaDigital firma)
        {
            await Init();
            return await _connection.UpdateAsync(firma);
        }

        //Read
        public async Task<List<Models.firmaDigital>> getListFirmas()
        {
            await Init();
            return await _connection.Table<firmaDigital>().ToListAsync();
        }

        //Read Element
        public async Task<Models.firmaDigital> getFirma(int pid)
        {
            await Init();
            return await _connection.Table<firmaDigital>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }

        //Delete
        public async Task<int> deleteFirma(int autorID)
        {
            await Init();
            var firmaToDelete = await getFirma(autorID);

            if (firmaToDelete != null)
            {
                return await _connection.DeleteAsync(firmaToDelete);
            }

            return 0;
        }
    }
}
