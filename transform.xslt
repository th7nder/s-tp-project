<?xml version="1.0"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:z="http://schemas.microsoft.com/2003/10/Serialization/" >
    <xsl:output method="html" indent="yes"/>
    <xsl:template match="text()|@*"/>

    <xsl:template name="attributes">
        <xsl:attribute name="class">group</xsl:attribute>
        <xsl:attribute name="opened">false</xsl:attribute>
        <xsl:attribute name="type">
            <xsl:value-of select="name()"/>
        </xsl:attribute>

        <xsl:if test="@z:Id != ''">
            <xsl:attribute name="id">
                <xsl:value-of select="concat('node_', @z:Id)"/>
            </xsl:attribute>
        </xsl:if>

        <xsl:if test="@z:Ref != ''">
            <xsl:attribute name="ref">
                <xsl:value-of select="concat('node_', @z:Ref)"/>
            </xsl:attribute>
        </xsl:if>
    </xsl:template>

    <xsl:template name="id_tester">
        #<xsl:value-of select="@z:Id"/><xsl:value-of select="@z:Ref"/>
    </xsl:template>

    <xsl:template match="Name|AccessModifier|ArrayRank|IsPointer|IsSealed|TypeKind|IsExtensionMethod|Kind|DefaultValue">
        <xsl:element name="div">
            <xsl:call-template name="attributes"/>
            <xsl:attribute name="class">node</xsl:attribute>

            <xsl:value-of select="name()"/>: <xsl:value-of select="."/>
        </xsl:element>
    </xsl:template>

    <xsl:template match="Types|Attributes|BaseTypes|Constructors|Fields|GenericArguments|Methods|Properties|TypeParameters|NestedTypes|Parameters|Namespaces|ExtensionMethods|Events|Accessors">
        <xsl:element name="div">
            <xsl:call-template name="attributes"/>

            <div class="node node-header"><xsl:value-of select="name()"/></div>
            <div class="node node-container">
                <xsl:apply-templates/>
            </div>
        </xsl:element>
    </xsl:template>

    <xsl:template match="Attribute|NamespaceMetadata|FieldMetadata|ConstructorMetadata|MethodParameterMetadata|MethodMetadata|Type|ReturnType|PropertyMetadata">
        <xsl:element name="div">
            <xsl:call-template name="attributes"/>

            <div class="node node-header"><xsl:value-of select="Name"/></div>
            <div class="node node-container">
                <xsl:apply-templates/>
            </div>
        </xsl:element>
    </xsl:template>

    <xsl:template match="TypeMetadata">
        <xsl:element name="div">
            <xsl:call-template name="attributes"/>

            <xsl:if test="IsPlaceholder = 'true'">
                <div class="node node-header"><xsl:value-of select="Name"/></div>
            </xsl:if>

            <xsl:if test="IsPlaceholder = 'false'">
                <div class="node node-header"><xsl:value-of select="Name"/></div>
                <div class="node node-container">
                    <xsl:apply-templates/>
                </div>
            </xsl:if>
        </xsl:element>
    </xsl:template>

    <xsl:template match="AssemblyMetadata">
        <html>
            <header>
                <meta charset="utf-8"/>
            </header>
            <body>
                <template id="source">
                    <div id="all">
                        <xsl:apply-templates/>
                    </div>
                </template>
                <div id="tree"/>
                <style>
                    .node {
                        padding: 5px;
                        background: #eee;
                        border: 1px solid #b3b3b3;
                        margin: 1px;
                        margin-left: 10px;
                    }

                    .node:empty {
                        display: none;
                    }

                    .node-header {
                        background: #bbb;
                    }

                    .group > .node-header:before {
                        content: "► ";
                    }

                    .group[opened='true'] > .node-header:before {
                        content: "⯆ ";
                    }

                    .group {
                        cursor: pointer;
                    }
                </style>

                <script>
                    const template = document.getElementById("source");
                    const source = document.importNode(template.content, true);
                    const tree = document.getElementById("tree");

                    function reveal(parent, id) {
                        const element = source.getElementById(id);
                        const oldNode = document.importNode(element, true);
                        const newNode = document.importNode(element, false);

                        // Fix empty header
                        function fixHeader(node) {
                            if (node.innerText === '') {
                                let id = node.getAttribute("id");
                                let targetElement = source.getElementById(id);
                                if (targetElement) {
                                    targetElement = targetElement.querySelector("div[type='Name']");
                                    id = targetElement.getAttribute("ref");
                                    targetElement = source.getElementById(id);

                                    if (targetElement) {
                                        node.querySelector(":scope > .node-header").innerText = targetElement.innerText;
                                    } else {
                                        console.log("Unable to find element: " + id);
                                    }
                                } else {
                                    console.log("Unable to find element: " + id);
                                }
                            }
                        }

                        // Add group header
                        const header = document.importNode(oldNode.querySelector(":scope > .node-header"), true);
                        newNode.appendChild(header);
                        fixHeader(newNode);

                        const oldContainer = oldNode.querySelector(":scope > .node-container");
                        if (oldContainer === null) {
                            return;
                        }

                        const newContainer = document.importNode(oldContainer, false);

                        for (const child of oldContainer.childNodes) {
                            const subIsTextOnly = child.childNodes.length === 1 &amp;&amp; child.childNodes[0].nodeType === 3;
                            const newChild = document.importNode(child, subIsTextOnly);

                            if (child.nodeType === 1) {
                                const label = child.querySelector(":scope > .node-header");
                                if (label !== null) {
                                    newChild.appendChild(document.importNode(label, true));
                                }

                                // Resolve references
                                if (newChild.hasAttribute("ref")) {
                                    const ref = newChild.getAttribute("ref");
                                    const targetElement = source.getElementById(ref);

                                    if (targetElement) {
                                        const header = targetElement.querySelector(":scope > .node-header");
                                        if (header) {
                                            const targetLabel = document.importNode(header, true);
                                            newChild.appendChild(targetLabel);
                                        } else {
                                            newChild.innerText = targetElement.innerText;
                                        }

                                        newChild.setAttribute("id", ref);
                                        newChild.removeAttribute("ref");
                                    } else {
                                        console.log("Unable to find element: " + ref);
                                    }
                                }

                                fixHeader(newChild);
                            }

                            newContainer.appendChild(newChild);
                        }

                        newNode.appendChild(newContainer);
                        newNode.setAttribute("opened", "true");

                        const existing = parent.querySelector("#"+newNode.id);
                        if (existing) {
                            existing.parentElement.replaceChild(newNode, existing);
                        } else {
                            parent.appendChild(newNode);
                        }

                        return newNode;
                    }

                    function close(node) {
                        const container = node.querySelector(":scope > .node-container");
                        while (container.firstChild) {
                            container.removeChild(container.firstChild);
                        }

                        node.setAttribute("opened", "false");
                    }

                    const namespaces = source.querySelector("div[type='Namespaces']");
                    reveal(tree, namespaces.id);

                    document.addEventListener('click', function (event) {
                        const target = event.target.parentElement;
                        if (target === null || !target.hasAttribute("opened")) {
                            return;
                        }

                        const isOpen = target.getAttribute("opened") === 'true';

                        if (isOpen) {
                            close(target);
                            return;
                        }

                        if (target.matches('.group')) {
                            reveal(target.parentElement, target.id);
                        }
                    }, false);
                </script>
            </body>
        </html>

    </xsl:template>
</xsl:stylesheet>
