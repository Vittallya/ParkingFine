using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IDeclarationService
    {
        void Clear();
        bool IsEdit { get; }

        bool CanEditDeclaration(Declaration dec);

        string Message { get; }

        Task<bool> ApplyDeclaration();

        bool IsPadied { get; }

        /// <summary>
        /// Получить заполненое заявление от VM
        /// </summary>
        /// <param name="declaration"></param>
        void SetupFilledDeclaration(DeclarationDto declaration);


        /// <summary>
        /// Получить заполненный профиль
        /// </summary>
        /// <param name="declaration"></param>
        void SetupFilledProfile(ProfileDto declaration);


        /// <summary>
        /// Возвращает или существующий (при редактировании), или новый
        /// </summary>
        /// <returns></returns>
        DeclarationDto GetDeclaration();
        int GetCost();

        /// <summary>
        /// Возвращает или существующий (при редактировании), или новый
        /// </summary>
        /// <returns></returns>
        ProfileDto GetProfile();

        /// <summary>
        /// Точка начала работы сервиса. Возращает результат, есть ли уже есть активное заявление
        /// </summary>
        /// <param name="evac">Экз. эвакуации, который нужно проверить</param>
        /// <returns></returns>
        Task<bool> SetupEvac(int evac);

        Task<IEnumerable<string>> GetTimesForDate(DateTime date);

        void DefineStatus(DecStatus decStatus);
    }
}
