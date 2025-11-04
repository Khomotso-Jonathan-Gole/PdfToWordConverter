/*
 * PDF to Word Converter
 * Developed by: Khomotso Jonathan Gole
 * Contact: 0783672051 / 0795886717
 * Email: khomotsojonathan2@gmail.com
 * LinkedIn: https://www.linkedin.com/in/khomotso-jonathan-gole
 * GitHub: https://github.com/Khomotso-Jonathan-Gole
 * 
 * Copyright (c) 2025 Khomotso Jonathan Gole. All rights reserved.
 * Licensed under the MIT License.
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace PdfToWordConverter
{
    public class ConversionResult
    {
        public bool Success { get; set; }
        public string OutputPath { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public long FileSize { get; set; }
    }

    public class PythonService
    {
        public static ConversionResult ConvertPdfToWord(string pdfPath, string outputDir = null)
        {
            try
            {
                // Validate input file
                if (!File.Exists(pdfPath))
                {
                    return new ConversionResult
                    {
                        Success = false,
                        Error = $"File not found: {pdfPath}"
                    };
                }

                // Determine output directory
                if (string.IsNullOrEmpty(outputDir))
                    outputDir = Path.GetDirectoryName(pdfPath);

                // Generate output file path
                string fileName = Path.GetFileNameWithoutExtension(pdfPath) + ".docx";
                string docxPath = Path.Combine(outputDir, fileName);

                // Get Python executable path
                string pythonExe = FindPythonExecutable();
                if (string.IsNullOrEmpty(pythonExe))
                {
                    return new ConversionResult
                    {
                        Success = false,
                        Error = "Python not found. Please install Python 3.8+"
                    };
                }

                // Get script path
                string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                               "PythonScripts", "extraction.py");

                if (!File.Exists(scriptPath))
                {
                    return new ConversionResult
                    {
                        Success = false,
                        Error = $"Python script not found: {scriptPath}"
                    };
                }

                MessageBox.Show($"Starting conversion:\nPython: {pythonExe}\nScript: {scriptPath}\nInput: {pdfPath}\nOutput: {docxPath}",
                              "Debug Info", MessageBoxButton.OK, MessageBoxImage.Information);

                // Run Python script
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = pythonExe,
                    Arguments = $"\"{scriptPath}\" \"{pdfPath}\" \"{docxPath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = false, // Changed to false to see console
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = start;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    // Wait with timeout
                    if (!process.WaitForExit(60000)) // 60 second timeout
                    {
                        process.Kill();
                        return new ConversionResult
                        {
                            Success = false,
                            Error = "Conversion timeout - process took too long"
                        };
                    }

                    MessageBox.Show($"Python process completed.\nExit Code: {process.ExitCode}\nOutput: {output}\nError: {error}",
                                  "Process Info", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (process.ExitCode == 0)
                    {
                        try
                        {
                            var result = JsonSerializer.Deserialize<ConversionResult>(output);
                            if (result != null)
                            {
                                return result;
                            }
                            else
                            {
                                // Check if file was created even if JSON parsing failed
                                if (File.Exists(docxPath))
                                {
                                    return new ConversionResult
                                    {
                                        Success = true,
                                        OutputPath = docxPath,
                                        FileSize = new FileInfo(docxPath).Length,
                                        Message = "Conversion completed (file created)"
                                    };
                                }
                            }
                        }
                        catch (Exception jsonEx)
                        {
                            // Check if file was created even if JSON parsing failed
                            if (File.Exists(docxPath))
                            {
                                return new ConversionResult
                                {
                                    Success = true,
                                    OutputPath = docxPath,
                                    FileSize = new FileInfo(docxPath).Length,
                                    Message = "Conversion completed (JSON parse failed but file created)"
                                };
                            }
                            return new ConversionResult
                            {
                                Success = false,
                                Error = $"Failed to parse Python response: {jsonEx.Message}\nRaw output: {output}"
                            };
                        }
                    }

                    return new ConversionResult
                    {
                        Success = false,
                        Error = $"Python script failed with exit code {process.ExitCode}\nError: {error}\nOutput: {output}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ConversionResult
                {
                    Success = false,
                    Error = $"Conversion failed: {ex.Message}"
                };
            }
        }

        private static string FindPythonExecutable()
        {
            // Check common Python installation paths
            string[] possiblePaths = {
                "python.exe",
                "python3.exe",
                "py.exe",
                @"C:\Python313\python.exe",
                @"C:\Python312\python.exe",
                @"C:\Python311\python.exe",
                @"C:\Python310\python.exe",
                @"C:\Python39\python.exe",
                @"C:\Python38\python.exe",
                @"C:\Program Files\Python313\python.exe",
                @"C:\Program Files\Python312\python.exe",
                @"C:\Program Files\Python311\python.exe",
                @"C:\Program Files\Python310\python.exe",
                @"C:\Program Files\Python39\python.exe",
                @"C:\Program Files\Python38\python.exe"
            };

            foreach (string path in possiblePaths)
            {
                try
                {
                    ProcessStartInfo start = new ProcessStartInfo
                    {
                        FileName = path,
                        Arguments = "--version",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true
                    };

                    using (Process process = Process.Start(start))
                    {
                        process.WaitForExit(5000);
                        if (process.ExitCode == 0)
                            return path;
                    }
                }
                catch
                {
                    // Continue searching
                }
            }

            return null;
        }
    }
}

