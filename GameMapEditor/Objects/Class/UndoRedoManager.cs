using GameMapEditor.Objects.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects
{
    // TODO : Corriger 
    public class UndoRedoManager
    {
        private const int MAX_LENGHT = 20;

        private LimitedStack<GameMap> undoStack;
        private LimitedStack<GameMap> redoStack;

        public event EventHandler<UndoRedoEventArgs> UndoHappened;
        public event EventHandler<UndoRedoEventArgs> RedoHappened;

        /// <summary>
        /// Créer un nouveau manager d'historique de modifications
        /// </summary>
        public UndoRedoManager()
        {
            this.undoStack = new LimitedStack<GameMap>(MAX_LENGHT);
            this.redoStack = new LimitedStack<GameMap>(MAX_LENGHT);
        }

        /// <summary>
        /// Ajoute une nouvelle étape dans l'historique
        /// </summary>
        /// <param name="map"></param>
        public void Add(GameMap map)
        {
            this.undoStack.Push(map.Clone() as GameMap);
            this.redoStack.Clear();
        }

        /// <summary>
        /// Effectue un "Annuler"
        /// </summary>
        public void Undo()
        {
            if (!this.CanUndo)
            {
                throw new IndexOutOfRangeException("Impossible d'effectuer l'opération d'annulation");
            }

            var value = this.undoStack.Pop();
            this.redoStack.Push(value);
            this.UndoHappened?.Invoke(this, new UndoRedoEventArgs(value));
        }

        // TODO : Réviser
        /// <summary>
        /// Effectue un "Refaire"
        /// </summary>
        public void Redo()
        {
            if (!this.CanRedo)
            {
                throw new IndexOutOfRangeException("Impossible d'effectuer l'opération de refonte");
            }

            var value = this.redoStack.Pop();
            this.undoStack.Push(value);
            this.RedoHappened?.Invoke(this, new UndoRedoEventArgs(value));
        }

        // TODO : Debug only
        public override string ToString() => $"Undo : {this.undoStack.Count}\nRedo : {this.redoStack.Count}";

        /// <summary>
        /// Obtient l'état de possibilité d'action du Undo
        /// </summary>
        public bool CanUndo
        {
            get { return this.undoStack.Count > 0; }
        }

        /// <summary>
        /// Obtient l'état de possibilité d'action du Redo
        /// </summary>
        public bool CanRedo
        {
            get { return this.redoStack.Count > 0; }
        }
    }
}
