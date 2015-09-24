using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects.Class
{
    class LimitedStack<T> : LinkedList<T>
    {
        private const uint DEFAULT_LIMIT = 20;

        private uint limit;

        /// <summary>
        /// Créer un stack d'objets avec une limite maximale d'objets
        /// </summary>
        /// <param name="limit">Le nombre d'objets maximal pouvant être pris en charge</param>
        public LimitedStack(uint limit = DEFAULT_LIMIT)
        {
            this.limit = limit;
        }

        /// <summary>
        /// Ajout l'item en haut de la pile et supprime l'item en bas si la limite de taille est dépassée
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            this.AddFirst(item);

            if (this.Count > limit)
            {
                this.RemoveLast();
            }
        }

        /// <summary>
        /// Dépile et retourne l'item en haut de la pile
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            var item = this.First.Value;
            this.RemoveFirst();
            return item;
        }
    }
}
