using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor.Compilation;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Editor
{
    public class ExampleConsoleLogs : MonoBehaviour
    {
        private static CancellationTokenSource _cts;
        // private void Awake()
        // {
        //     LogServerErrors();
        //     LogCodeErrors();
        //     StartLoopFunction();
        //     ShowAllWarnings();
        // }
        //
        // private void OnDestroy()
        // {
        //     StopLoopFunction();
        // }

        public void LogServerErrors()
        {
            ThrowException(() => throw new HttpRequestException());
            ThrowException(() => throw new HttpListenerException());
        }

        public static void LogAllTypes()
        {
            Debug.LogError($"Error log: {DateTime.Now}");
            Debug.LogWarning($"Warning log: {DateTime.Now}");
            Debug.Log($"Info log: {DateTime.Now}");
        }

        public static void LogMess()
        {
            LobMessageByType("Player has connected", LogLevel.Info);
            LobMessageByType("Player disconnected unexpectedly", LogLevel.Warn);
            LobMessageByType("Player tried to use an unavailable feature", LogLevel.Error);
            LobMessageByType("Resource loaded successfully", LogLevel.Info);
            LobMessageByType("Invalid input detected", LogLevel.Warn);
            LobMessageByType("Failed to load asset 'Character.prefab'", LogLevel.Error);
            LobMessageByType("Physics engine initialized", LogLevel.Info);
            LobMessageByType("AI system could not find a path", LogLevel.Warn);
            LobMessageByType("GameObject 'Enemy' is missing a Rigidbody component", LogLevel.Error);
            LobMessageByType("User selected difficulty 'Hard'", LogLevel.Info);
            LobMessageByType("Player saved game progress", LogLevel.Info);
            LobMessageByType("Network latency spike detected", LogLevel.Warn);
            LobMessageByType("Database connection lost", LogLevel.Error);
            LobMessageByType("Asset bundle 'Textures' loaded", LogLevel.Info);
            LobMessageByType("Player character health is low", LogLevel.Warn);
            LobMessageByType("Could not connect to the server", LogLevel.Error);
            LobMessageByType("Game paused by user", LogLevel.Info);
            LobMessageByType("Player tried to access a restricted area", LogLevel.Warn);
            LobMessageByType("Render pipeline encountered an issue", LogLevel.Error);
            LobMessageByType("Level 'Forest' loaded successfully", LogLevel.Info);
        }

        private static void LobMessageByType(string message, LogLevel logType)
        {
            switch (logType)
            {
                case LogLevel.Error:
                    Debug.LogError($"Error log: {message}");
                    break;
                case LogLevel.Warn:
                    Debug.LogWarning($"Warning log: {message}");
                    break;
                case LogLevel.Info:
                case LogLevel.Verbose:
                case LogLevel.Debug:
                case LogLevel.Silly:
                default:
                    Debug.Log($"Info log: {message}");
                    break;
            }
        }

        public void LogCodeErrors()
        {
            ThrowException(() =>
                throw new AssemblyDefinitionException(
                    "Simulated compilation error: Assembly definition overflow detected."));
            ThrowException(() =>
                throw new TargetInvocationException(new Exception("Simulated compilation error: Invocation failed.")));
            ThrowException(() => throw new SyntaxErrorException());
        }

        public void ShowAllWarnings()
        {
            // 1. Nieużywana zmienna
            Debug.LogWarning("The variable 'xyz' is assigned but its value is never used.");

            // 2. Przestarzałe API (Obsolete API)
            Debug.LogWarning(
                "XYZ() is obsolete: This function is deprecated and will be removed in future versions of Unity.");

            // 3. Brak przypisanej referencji (Null Reference)
            Debug.LogWarning("The referenced script on this Behavior is missing!");

            // 4. Nieprawidłowe użycie fizyki
            Debug.LogWarning("Physics.Raycast should only be called in FixedUpdate when using rigidbodies.");

            // 5. Rozmiar tekstur i materiałów
            Debug.LogWarning("Texture size is not power of two, which might cause issues on some platforms.");

            // 6. Zmiany w hierarchii podczas renderingu
            Debug.LogWarning(
                "Changing the Transform of a RectTransform during rendering may cause performance issues.");

            // 7. Brak komponentów
            Debug.LogWarning("The referenced script is missing.");

            // 8. Zła konfiguracja oświetlenia
            Debug.LogWarning("Lighting settings are invalid.");

            // 9. Problemy z animacją
            Debug.LogWarning("Animator is not playing any state.");

            // 10. Problemy z kompilacją
            Debug.LogWarning("The script XYZ uses UnityEditor which is not available when building the game.");
        }

        private void ThrowException(Action func)
        {
            try
            {
                func();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        private static async UniTask LogOneErrorInLoop(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Debug.LogError(
                    new InvalidOperationException("Simulated compilation error: Invalid operation detected."));
                await UniTask.Delay(100, cancellationToken: ct);
            }
        }

        public static void StopLoopFunction()
        {
            _cts.Cancel();
        }

        public static void StartLoopFunction()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            LogOneErrorInLoop(_cts.Token).Forget();
        }

        public void LogInfo()
        {
            var info = new SystemException();
            Debug.Log(info);
        }


        public static void LogRandomAppStatus()
        {
            // Generowanie 20 przykładowych logów
            LobMessageByType("[RetentionCalendar] All days claimed.", GetRandomLogLevel());
            LobMessageByType("[InAppPurchase] Purchase completed successfully.", GetRandomLogLevel());
            LobMessageByType("[LoginSystem] User login failed due to incorrect password.", GetRandomLogLevel());
            LobMessageByType("[PushNotifications] Push notification received.", GetRandomLogLevel());
            LobMessageByType("[UserSession] Session expired, redirecting to login.", GetRandomLogLevel());
            LobMessageByType("[Analytics] Event 'level_completed' sent.", GetRandomLogLevel());
            LobMessageByType("[FriendSystem] Friend request sent to user: JohnDoe.", GetRandomLogLevel());
            LobMessageByType("[Achievements] New achievement unlocked: 'First Win'.", GetRandomLogLevel());
            LobMessageByType("[Leaderboard] Failed to update leaderboard data.", GetRandomLogLevel());
            LobMessageByType("[InAppPurchase] Purchase failed: insufficient funds.", GetRandomLogLevel());
            LobMessageByType("[LoginSystem] User logged in successfully.", GetRandomLogLevel());
            LobMessageByType("[RetentionCalendar] Daily reward claimed: 50 coins.", GetRandomLogLevel());
            LobMessageByType("[PushNotifications] Failed to register device for push notifications.",
                GetRandomLogLevel());
            LobMessageByType("[UserSession] Session extended by 30 minutes.", GetRandomLogLevel());
            LobMessageByType("[Analytics] Error sending event 'user_signup'.", GetRandomLogLevel());
            LobMessageByType("[FriendSystem] Failed to send friend request due to network error.", GetRandomLogLevel());
            LobMessageByType("[Achievements] Achievement 'Master Explorer' progress updated.", GetRandomLogLevel());
            LobMessageByType("[Leaderboard] Leaderboard updated successfully.", GetRandomLogLevel());
            LobMessageByType("[InAppPurchase] Promo code applied: 20% discount.", GetRandomLogLevel());
            LobMessageByType("[LoginSystem] Two-factor authentication enabled for user.", GetRandomLogLevel());
        }

        private static LogLevel GetRandomLogLevel()
        {
            // Losowanie poziomu logu
            Array logLevels = Enum.GetValues(typeof(LogLevel));
            return (LogLevel) logLevels.GetValue(UnityEngine.Random.Range(0, logLevels.Length));
        }
    }
}