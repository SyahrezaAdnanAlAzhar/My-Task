using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTaskLibrary
{
    public enum TaskState { Done, InProgress, PostPone, ToDo };
    public enum TriggerTaskState { Menyelesaikan, Mengerjakan, Menunda };

    public class TaskTransition
    {
        public TaskState prevState;
        public TaskState nextState;
        public TaskState currentState;
        public TriggerTaskState triggerTaskState;
        public TaskTransition(TaskState prevState, TaskState nextState, TriggerTaskState trigger)
        {
            this.prevState = prevState;
            this.nextState = nextState;
            this.triggerTaskState = trigger;
        }
        private static TaskTransition[] transitions =
        {
            new TaskTransition(TaskState.ToDo, TaskState.InProgress, TriggerTaskState.Mengerjakan),
            new TaskTransition(TaskState.InProgress, TaskState.PostPone, TriggerTaskState.Menunda),
            new TaskTransition(TaskState.PostPone, TaskState.InProgress, TriggerTaskState.Mengerjakan),
            new TaskTransition(TaskState.InProgress, TaskState.Done, TriggerTaskState.Menyelesaikan)
        };

        public TaskState getNextState(TaskState prevState, TriggerTaskState trigger)
        {
            TaskState nextState = prevState;
            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].prevState == prevState && transitions[i].triggerTaskState == trigger)
                {
                    nextState = transitions[i].nextState;
                }
            }
            return nextState;
        }
    }
}
