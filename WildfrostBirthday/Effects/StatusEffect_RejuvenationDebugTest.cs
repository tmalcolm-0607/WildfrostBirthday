namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_RejuvenationDebugTest
    {
        public static void TestImagePaths(WildFamilyMod mod)
        {
            var imagePath = mod.ImagePath("status/rejuvenation.png");
            UnityEngine.Debug.Log($"[RejuvenationDebug] Image path: {imagePath}");
            UnityEngine.Debug.Log($"[RejuvenationDebug] File exists: {System.IO.File.Exists(imagePath)}");
            
            // Test alternative paths
            var altPath1 = mod.ImagePath("status\\rejuvenation.png");
            UnityEngine.Debug.Log($"[RejuvenationDebug] Alt path 1: {altPath1}, exists: {System.IO.File.Exists(altPath1)}");
            
            var altPath2 = mod.ImagePath("rejuvenation.png");
            UnityEngine.Debug.Log($"[RejuvenationDebug] Alt path 2: {altPath2}, exists: {System.IO.File.Exists(altPath2)}");
            
            // List files in status directory
            var statusDir = System.IO.Path.GetDirectoryName(imagePath);
            if (System.IO.Directory.Exists(statusDir))
            {
                var files = System.IO.Directory.GetFiles(statusDir, "*.png");
                UnityEngine.Debug.Log($"[RejuvenationDebug] Files in {statusDir}: {string.Join(", ", files)}");
            }
            else
            {
                UnityEngine.Debug.LogError($"[RejuvenationDebug] Status directory does not exist: {statusDir}");
            }
        }
    }
}
