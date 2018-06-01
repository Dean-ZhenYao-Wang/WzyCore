using System;
using System.Collections.Generic;
using System.Text;

namespace WzyCore.Environment.Shell
{
    public class ShellOptions
    {
        /// <summary>
        /// 根容器
        /// The root container
        /// </summary>
        public string ShellsApplicationDataPath { get; set; }

        /// <summary>
        /// 该容器的Shells
        /// The container for shells
        /// </summary>
        public string ShellsContainerName { get; set; }
    }
}
