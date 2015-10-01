using GameMapEditor.Objects.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects
{
    public class UndoRedoManager<T>
    {
        public delegate void UndoRedoEventArgs(object sender, T e);

        private const int MAX_LENGHT = 20;

        private LimitedStack<T> undoStack;
        private LimitedStack<T> redoStack;

        public event UndoRedoEventArgs UndoHappened;
        public event UndoRedoEventArgs RedoHappened;

        /// <summary>
        /// Créer un nouveau manager d'historique de modifications
        /// </summary>
        public UndoRedoManager()
        {
            this.undoStack = new LimitedStack<T>(MAX_LENGHT);
            this.redoStack = new LimitedStack<T>(MAX_LENGHT);
        }

        /// <summary>
        /// Ajoute une nouvelle étape dans l'historique
        /// </summary>
        /// <param name="obj">L'ancienne valeur avant modification</param>
        public void Add(T obj)
        {
            this.undoStack.Push(obj);
            this.redoStack.Clear();
        }

        /// <summary>
        /// Réalise un undo auprès du manager
        /// </summary>
        /// <param name="obj">L'élément courant</param>
        public void Undo(T obj)
        {
            if (!this.CanUndo)
            {
                throw new IndexOutOfRangeException("Impossible d'effectuer l'opération d'annulation");
            }

            var value = this.undoStack.Pop();
            this.redoStack.Push(obj);
            this.UndoHappened?.Invoke(this, value);
        }

        /// <summary>
        /// Réalise un redo auprès du manager
        /// </summary>
        /// <param name="obj">L'élément courant</param>
        public void Redo(T obj)
        {
            if (!this.CanRedo)
            {
                throw new IndexOutOfRangeException("Impossible d'effectuer l'opération de refonte");
            }

            var value = this.redoStack.Pop();
            this.undoStack.Push(obj);
            this.RedoHappened?.Invoke(this, value);
        }

        /// <summary>
        /// Obtient l'état de possibilité d'action du Undo
        /// </summary>
        public bool CanUndo => this.undoStack.Count > 0;

        /// <summary>
        /// Obtient l'état de possibilité d'action du Redo
        /// </summary>
        public bool CanRedo => this.redoStack.Count > 0;
    }
}
