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
        // TODO : Implémenter la limite
        private const int MAX_LENGHT = 6;

        private Stack<GameMap> undoStack;
        private Stack<GameMap> redoStack;

        public event EventHandler<UndoRedoEventArgs> UndoHappened;
        public event EventHandler<UndoRedoEventArgs> RedoHappened;

        public UndoRedoManager()
        {
            this.undoStack = new Stack<GameMap>();
            this.redoStack = new Stack<GameMap>();
        }

        public void Add(GameMap map)
        {
            GameMap mapCloned = map.Clone();

            this.undoStack.Push(mapCloned);
            this.redoStack.Clear();
        }

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

        public bool CanUndo
        {
            get { return this.undoStack.Count > 0; }
        }

        public bool CanRedo
        {
            get { return this.redoStack.Count > 0; }
        }
    }
}
