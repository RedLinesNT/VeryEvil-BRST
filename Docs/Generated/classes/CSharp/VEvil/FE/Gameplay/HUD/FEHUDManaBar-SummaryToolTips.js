﻿NDSummary.OnToolTipsLoaded("CSharpClass:VEvil.FE.Gameplay.HUD.FEHUDManaBar",{764:"<div class=\"NDToolTip TClass LCSharp\"><div class=\"NDClassPrototype\" id=\"NDClassPrototype764\"><div class=\"CPEntry TClass Current\"><div class=\"CPModifiers\"><span class=\"SHKeyword\">public</span></div><div class=\"CPName\"><span class=\"Qualifier\">VEvil.&#8203;FE.&#8203;Gameplay.&#8203;HUD.</span>&#8203;FEHUDManaBar</div></div></div><div class=\"TTSummary\">FEHUDManaBar (Front End Heads-Up Display Mana Bar) is a component part of the HeadsUpDisplay, displaying the current mana of a ManaContainer</div></div>",766:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype766\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHMetadata\">[Header(&quot;FEHUD Mana Bar - References&quot;)]</span></div><div class=\"PSection PPlainSection\"><span class=\"SHMetadata\">[SerializeField, Tooltip(&quot;The Image on the canvas displaying the current mana of the targeted ManaContainer.&quot;)]</span></div><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private</span> Image filler</div></div></div>",767:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype767\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHMetadata\">[Header(&quot;FEHUD Mana Bar - Settings&quot;)]</span></div><div class=\"PSection PPlainSection\"><span class=\"SHMetadata\">[SerializeField, Tooltip(&quot;Amount of smoothing applied to the mana bar.&quot;)]</span></div><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private float</span> smoothing</div></div></div>",768:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype768\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private float</span> fillerVelocity</div></div></div>",770:"<div class=\"NDToolTip TProperty LCSharp\"><div id=\"NDPrototype770\" class=\"NDPrototype WideForm\"><div class=\"PSection PParameterSection CStyle\"><div class=\"PParameterCells\" data-WideColumnCount=\"4\" data-NarrowColumnCount=\"3\"><div class=\"PBeforeParameters RightSpaceOnWide\" data-WideGridArea=\"1/1/3/2\" data-NarrowGridArea=\"1/1/2/4\" style=\"grid-area:1/1/3/2\"><span class=\"SHKeyword\">public</span> ManaContainer ManaContainer {</div><div class=\"PName InLastParameterColumn\" data-WideGridArea=\"1/3/2/4\" data-NarrowGridArea=\"2/2/3/3\" style=\"grid-area:1/3/2/4\"><span class=\"SHKeyword\">get</span>;</div><div class=\"PModifierQualifier InFirstParameterColumn\" data-WideGridArea=\"2/2/3/3\" data-NarrowGridArea=\"3/1/4/2\" style=\"grid-area:2/2/3/3\"><span class=\"SHKeyword\">private</span>&nbsp;</div><div class=\"PName InLastParameterColumn\" data-WideGridArea=\"2/3/3/4\" data-NarrowGridArea=\"3/2/4/3\" style=\"grid-area:2/3/3/4\"><span class=\"SHKeyword\">set</span></div><div class=\"PAfterParameters\" data-WideGridArea=\"2/4/3/5\" data-NarrowGridArea=\"4/1/5/4\" style=\"grid-area:2/4/3/5\">}</div></div></div></div><div class=\"TTSummary\">The ManaContainer this FEHUDManaBar is watching.</div></div>",771:"<div class=\"NDToolTip TProperty LCSharp\"><div id=\"NDPrototype771\" class=\"NDPrototype WideForm\"><div class=\"PSection PParameterSection CStyle\"><div class=\"PParameterCells\" data-WideColumnCount=\"4\" data-NarrowColumnCount=\"3\"><div class=\"PBeforeParameters RightSpaceOnWide\" data-WideGridArea=\"1/1/3/2\" data-NarrowGridArea=\"1/1/2/4\" style=\"grid-area:1/1/3/2\"><span class=\"SHKeyword\">public float</span> TargetFillerValue {</div><div class=\"PName InLastParameterColumn\" data-WideGridArea=\"1/3/2/4\" data-NarrowGridArea=\"2/2/3/3\" style=\"grid-area:1/3/2/4\"><span class=\"SHKeyword\">get</span>;</div><div class=\"PModifierQualifier InFirstParameterColumn\" data-WideGridArea=\"2/2/3/3\" data-NarrowGridArea=\"3/1/4/2\" style=\"grid-area:2/2/3/3\"><span class=\"SHKeyword\">private</span>&nbsp;</div><div class=\"PName InLastParameterColumn\" data-WideGridArea=\"2/3/3/4\" data-NarrowGridArea=\"3/2/4/3\" style=\"grid-area:2/3/3/4\"><span class=\"SHKeyword\">set</span></div><div class=\"PAfterParameters\" data-WideGridArea=\"2/4/3/5\" data-NarrowGridArea=\"4/1/5/4\" style=\"grid-area:2/4/3/5\">}</div></div></div></div><div class=\"TTSummary\">The target/desired filler amount the filler should have.</div></div>",772:"<div class=\"NDToolTip TProperty LCSharp\"><div id=\"NDPrototype772\" class=\"NDPrototype WideForm\"><div class=\"PSection PParameterSection CStyle\"><div class=\"PParameterCells\" data-WideColumnCount=\"4\" data-NarrowColumnCount=\"3\"><div class=\"PBeforeParameters RightSpaceOnWide\" data-WideGridArea=\"1/1/3/2\" data-NarrowGridArea=\"1/1/2/4\" style=\"grid-area:1/1/3/2\"><span class=\"SHKeyword\">public float</span> CurrentFillerValue {</div><div class=\"PName InLastParameterColumn\" data-WideGridArea=\"1/3/2/4\" data-NarrowGridArea=\"2/2/3/3\" style=\"grid-area:1/3/2/4\"><span class=\"SHKeyword\">get</span>;</div><div class=\"PModifierQualifier InFirstParameterColumn\" data-WideGridArea=\"2/2/3/3\" data-NarrowGridArea=\"3/1/4/2\" style=\"grid-area:2/2/3/3\"><span class=\"SHKeyword\">private</span>&nbsp;</div><div class=\"PName InLastParameterColumn\" data-WideGridArea=\"2/3/3/4\" data-NarrowGridArea=\"3/2/4/3\" style=\"grid-area:2/3/3/4\"><span class=\"SHKeyword\">set</span></div><div class=\"PAfterParameters\" data-WideGridArea=\"2/4/3/5\" data-NarrowGridArea=\"4/1/5/4\" style=\"grid-area:2/4/3/5\">}</div></div></div></div><div class=\"TTSummary\">The current filler amount the filler have.</div></div>",774:"<div class=\"NDToolTip TFunction LCSharp\"><div id=\"NDPrototype774\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private void</span> Update()</div></div></div>"});