using UnityEngine.Networking;

namespace HyperPlugin {

  public static class UnityWebRequestAsyncOperationExtension {

    public static UnityWebRequestAsyncOperationAwaiter GetAwaiter(this UnityWebRequestAsyncOperation operation) {
      return new UnityWebRequestAsyncOperationAwaiter(operation);
    }
  }

}