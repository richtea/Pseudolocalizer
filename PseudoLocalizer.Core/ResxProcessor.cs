﻿namespace PseudoLocalizer.Core
{
    using System.Xml;

    /// <summary>
    /// Applies transforms to string values in Resx resource files.
    /// </summary>
    public sealed class ResxProcessor : XmlProcessor
    {
        /// <inheritdoc />
        protected override XmlNodeList SelectNodes(XmlDocument document, XmlNamespaceManager nsmgr)
            => document.SelectNodes("/root/data/value");

        /// <inheritdoc />
        protected override void Visit(XmlNode node, XmlNamespaceManager nsmgr)
        {
            var child = node.FirstChild;
            if (child != null && child.NodeType == XmlNodeType.Text)
            {
                var original = child.Value;
                var transformed = Transform(original);

                if (transformed != original)
                {
                    child.Value = transformed;
                }
            }
        }
    }
}
