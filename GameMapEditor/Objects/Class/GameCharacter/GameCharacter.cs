using GameMapEditor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor.Objects.Class.GameCharacter
{
    public class GameCharacter
    {
        private static GameDirection DEFAULT_DIRECTION = GameDirection.Down;
        private static GameVelocity DEFAULT_VELOCITY = GameVelocity.Medium;
        private const int DEFAULT_ANIMATION_MARK = 0;
        private const int DEFAULT_ANIMATION_TIMEWAIT = 200;
        private const int DEFAULT_INACTIVE_TIMEWAIT = 200;

        private Bitmap texture;
        private Rectangle srcRectangle;
        private GameVector2 position;
        private GameVector2 futurPosition;
        private GameVector2 margin;
        private GameDirection direction;
        private GameVelocity velocity;
        private int animationMark;
        private List<GameDirection> movementPath;
        private GameTimer animationTimer;
        private GameTimer timeleftTimer;

        public GameCharacter()
        {
            this.CanMove = true;

            this.texture = Resources.Sprite1;
            this.srcRectangle = new Rectangle(0, 0, this.texture.Size.Width / 4, this.texture.Size.Height / 4);
            this.margin = new GameVector2(0, -25);
            this.position = new GameVector2() + this.margin;
            this.futurPosition = this.position;

            this.direction = DEFAULT_DIRECTION;
            this.velocity = DEFAULT_VELOCITY;
            this.animationMark = DEFAULT_ANIMATION_MARK;
            this.movementPath = new List<GameDirection>();
            this.animationTimer = new GameTimer();
            this.timeleftTimer = new GameTimer();
        }

        protected void Move(GameDirection direction)
        {
            if(this.CanMove)
            {
                this.direction = direction;
                this.futurPosition = this.position + this.direction.Vector * 32;
                this.CanMove = false;
            }
        }

        public void MoveTo(GameDirection direction)
        {
            this.movementPath.Clear();
            this.Move(direction);
        }

        public void Update(float elapsedTime)
        {
            // Déplacement de l'entité selon son chemin prédéfini
            if (this.movementPath.Count > 0 & this.CanMove)
            {
                this.Move(this.movementPath.ElementAt(0));
                this.movementPath.RemoveAt(0);
            }

            if (!this.position.Equals(this.futurPosition) & !this.CanMove)
            {
                // Déplacements
                this.SyncMovementHandler(elapsedTime);

                // Animation de déplacement
                if (this.animationTimer.AsyncWait(DEFAULT_ANIMATION_TIMEWAIT / ((int)this.velocity / 50)))
                {
                    this.animationMark = this.animationMark >= 3 ? 0 : this.animationMark + 1;
                }
            }

            if (this.position.Equals(this.futurPosition))
            {
                // Rend de nouveau possible les futurs déplacements de l'entité
                if (!this.CanMove)
                {
                    this.CanMove = true;
                }

                // Mise à jour de l'inactivité de l'entité
                if (this.timeleftTimer.AsyncWait(DEFAULT_INACTIVE_TIMEWAIT))
                {
                    this.animationMark = 0;
                }
            }

            // Mise à jour de l'affichage de l'entité
            this.srcRectangle.X = this.animationMark * this.texture.Width / 4;
            this.srcRectangle.Y = this.direction.Index * this.texture.Height / 4;
        }

        public void Draw(GameVector2 mapOrigin, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.texture, new Rectangle(this.position.X - mapOrigin.X, this.position.Y - mapOrigin.Y, this.srcRectangle.Width, this.srcRectangle.Height), this.srcRectangle, GraphicsUnit.Pixel);
        }

        private void SyncMovementHandler(float elapsedTime)
        {
            // Déplacement de la position de l'entité
            // Synchronisation des temps de déplacement
            this.position += this.direction.Vector * (int)this.velocity/* * elapsedTime**/;

            if ((this.direction.Equals(GameDirection.Down)))
            {
                if ((this.position.Y > this.futurPosition.Y))
                {
                    this.position = new GameVector2(this.position.X, this.futurPosition.Y);
                }
            }
            else if ((this.direction.Equals(GameDirection.Left)))
            {
                if ((this.position.X < this.futurPosition.X))
                {
                    this.position = new GameVector2(this.futurPosition.X, this.position.Y);
                }
            }
            else if ((this.direction.Equals(GameDirection.Right)))
            {
                if ((this.position.X > this.futurPosition.X))
                {
                    this.position = new GameVector2(this.futurPosition.X, this.position.Y);
                }
            }
            else if ((this.direction.Equals(GameDirection.Up)))
            {
                if ((this.position.Y < this.futurPosition.Y))
                {
                    this.position = new GameVector2(this.position.X, this.futurPosition.Y);
                }
            }
        }

        public bool CanMove
        {
            get;
            private set;
        }
    }
}
