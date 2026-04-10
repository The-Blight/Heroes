using System.Collections.Generic;
using Heroes.Core.Heroes;

namespace Heroes.Core.Repositories.Interfaces;


/// <summary>
/// Интерфейс для сохранения и загрузки состояний героев
/// </summary>
public interface IHeroRepository
{
    /// <summary>
    /// Сохраняет список героев
    /// </summary>
    /// <param name="heroes">Список героев</param>
    /// <param name="saveName">Имя для сохранения</param>
    void Save(IEnumerable<HeroBase> heroes, string saveName);

    
    /// <summary>
    /// Загружает список героев из сохранения
    /// </summary>
    /// <param name="saveName">Имя сохранения</param>
    /// <returns>Список сохранений</returns>
    IEnumerable<HeroBase> Load(string saveName); 
}