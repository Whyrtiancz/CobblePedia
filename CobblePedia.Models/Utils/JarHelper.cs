namespace CobblePedia.Models.Utils
{
    using System;
    using System.IO.Compression;

    public static class JarHelper
    {
        /// <summary>
        /// On prend le jar puis on en extrait les dossiers :
        /// /data/cobblemon/spawn_prof_world/*
        /// /data/cobblemon/species/*
        /// /assets/cobblemon/lang/fr-fr et en-us
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="extractRoot"></param>
        /// <param name="paths"></param>
        /// <param name="overwrite"></param>
        public static void ExtractSubfolder(string zipPath, string extractRoot, string[] paths, bool overwrite = false)
        {
            // Normaliser et assurer un séparateur final
            extractRoot = Path.GetFullPath(extractRoot);
            if (!extractRoot.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                extractRoot += Path.DirectorySeparatorChar;

            using var archive = ZipFile.OpenRead(zipPath);
            foreach (var entry in archive.Entries)
            {
                // Filtrer un sous-dossier interne du ZIP (chemin interne sensible à '/')
                bool found = false;
                foreach (string path in paths)
                {
                    found = found || entry.FullName.StartsWith(path, StringComparison.Ordinal);
                }
                if (!found)
                {
                    continue;
                }

                // Ignorer les "entrées dossier" (notées avec un suffixe '/')
                if (entry.FullName.EndsWith("/", StringComparison.Ordinal))
                    continue;

                var destinationPath = Path.GetFullPath(Path.Combine(extractRoot, entry.FullName));

                // Protection zip-slip: la destination doit rester sous extractRoot
                if (!destinationPath.StartsWith(extractRoot, StringComparison.Ordinal))
                    continue;

                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
                entry.ExtractToFile(destinationPath, overwrite);
            }
        }
    }
}
