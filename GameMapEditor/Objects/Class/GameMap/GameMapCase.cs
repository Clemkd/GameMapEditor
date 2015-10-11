using GameMapEditor.Objects.Class.GameCharacter;
using GameMapEditor.Objects.Class.GameMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects.Class
{
    public abstract class GameMapCase
    {
        private bool WalkingUnauthorizedFromBottom => 
            this.Type == BlockType.Full ||
            this.Type == BlockType.Down ||
            this.Type == BlockType.LeftDown ||
            this.Type == BlockType.LeftUpDown ||
            this.Type == BlockType.RightDown ||
            this.Type == BlockType.RightLeftDown ||
            this.Type == BlockType.RightUpDown ||
            this.Type == BlockType.UpDown;

        private bool WalkingUnauthorizedFromTop =>
            this.Type == BlockType.Full ||
            this.Type == BlockType.Up ||
            this.Type == BlockType.LeftUp ||
            this.Type == BlockType.LeftUpDown ||
            this.Type == BlockType.RightLeftUp ||
            this.Type == BlockType.RightUp ||
            this.Type == BlockType.RightUpDown ||
            this.Type == BlockType.UpDown;

        private bool WalkingUnauthorizedFromRight =>
            this.Type == BlockType.Full ||
            this.Type == BlockType.Right ||
            this.Type == BlockType.RightDown ||
            this.Type == BlockType.RightLeft ||
            this.Type == BlockType.RightLeftUp ||
            this.Type == BlockType.RightUp ||
            this.Type == BlockType.RightUpDown ||
            this.Type == BlockType.RightLeftDown;

        private bool WalkingUnauthorizedFromLeft =>
            this.Type == BlockType.Full ||
            this.Type == BlockType.Left ||
            this.Type == BlockType.LeftUp ||
            this.Type == BlockType.LeftUpDown ||
            this.Type == BlockType.RightLeftUp ||
            this.Type == BlockType.LeftDown ||
            this.Type == BlockType.RightLeft ||
            this.Type == BlockType.RightLeftDown;

        /// <summary>
        /// Obtient l'autorisation ou l'interdiction de marcher sur la case depuis un sens de déplacement spécifié
        /// </summary>
        /// <param name="walkingDirection">Le sens de déplacement relatif</param>
        /// <returns>Vrai si le déplacement est autorisé et False dans la cas contraire</returns>
        public bool IsWalkingAllowed(GameDirection walkingDirection)
        {
            // Si le type est "None", marche autorisée
            if (this.Type == BlockType.None)
                return true;

            switch(walkingDirection.Index)
            {
                case 0:
                    return !this.WalkingUnauthorizedFromBottom;
                case 1: 
                    // Déplacement relatif "Left", il faut donc regarder l'accés "Right"
                    return !this.WalkingUnauthorizedFromRight;
                case 2:
                    // Déplacement relatif "Right", il faut donc regarder l'accés "Left"
                    return !this.WalkingUnauthorizedFromLeft;
                case 3:
                    return !this.WalkingUnauthorizedFromTop;
            }

            return false;
        }

        /// <summary>
        /// Définit ou obtient le type de la case
        /// </summary>
        public BlockType Type
        {
            get;
            set;
        }
    }
}
