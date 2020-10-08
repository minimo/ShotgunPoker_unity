using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace HyperPlugin {

  public class UnityWebRequestAsyncOperationAwaiter : INotifyCompletion {

    private UnityWebRequestAsyncOperation operation;

    public UnityWebRequestAsyncOperationAwaiter(UnityWebRequestAsyncOperation operation) {
      this.operation = operation;
    }

    public bool IsCompleted {
      get { return operation.isDone; }
    }

    public void GetResult() { }

    public void OnCompleted(Action continuation) {
      operation.completed += _ => continuation();
    }

  }

}