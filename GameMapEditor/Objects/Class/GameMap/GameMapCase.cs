using GameMapEditor.Objects.Enumerations;
using System;

namespace GameMapEditor.Objects.Class
{
    public class GameMapCase
    {
        /// <summary>
        /// Définit ou obtient le type de la case
        /// </summary>
        public CaseType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Determine si la case possède le type donné
        /// </summary>
        /// <param name="typeRef">Le type à comparer</param>
        /// <returns>True si le type à comparer correspond à l'un des types de la case, False dans le cas contraire</returns>
        public bool IsType(CaseType typeRef)
        {
            return Convert.ToBoolean(((int)this.Type >> (int)Math.Log((int)typeRef, 2)) & 1);
        }
    }
}
