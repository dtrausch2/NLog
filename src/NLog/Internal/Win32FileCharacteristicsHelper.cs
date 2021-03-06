// 
// Copyright (c) 2004-2018 Jaroslaw Kowalski <jaak@jkowalski.net>, Kim Christensen, Julian Verdurmen
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

#if !SILVERLIGHT && !__IOS__ && !__ANDROID__ && !NETSTANDARD

namespace NLog.Internal
{
    using System;
    using System.IO;

    /// <summary>
    /// Win32-optimized implementation of <see cref="FileCharacteristicsHelper"/>.
    /// </summary>
    internal class Win32FileCharacteristicsHelper : FileCharacteristicsHelper
    {
        /// <summary>
        /// Gets the information about a file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <returns>The file characteristics, if the file information was retrieved successfully, otherwise null.</returns>
        public override FileCharacteristics GetFileCharacteristics(string fileName, FileStream fileStream)
        {
            if (fileStream != null)
            {
                Win32FileNativeMethods.BY_HANDLE_FILE_INFORMATION fileInfo;
                if (Win32FileNativeMethods.GetFileInformationByHandle(fileStream.SafeFileHandle.DangerousGetHandle(), out fileInfo))
                    return new FileCharacteristics(DateTime.FromFileTimeUtc(fileInfo.ftCreationTime), DateTime.FromFileTimeUtc(fileInfo.ftLastWriteTime), fileInfo.nFileSizeLow + (((long)fileInfo.nFileSizeHigh) << 32));
            }
            return null;
        }
    }
}

#endif
