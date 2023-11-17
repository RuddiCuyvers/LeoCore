using System;
using System.IO;
using System.Reflection;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace WGK.Lib.Validation.Rules
{
    public static class RuleSetManager
    {
        /// <summary>
        /// Load a .rules resource (= manifestResource) from an assembly and
        /// apply the specified RuleSet against a target instance of a specified type
        /// of object.
        /// </summary>
        /// <param name="pRulesAssemblyType">Any type in the rules assembly</param>
        /// <param name="pRulesManifestResource"></param>
        /// <param name="pRuleSetName"></param>
        /// <param name="pTargetType"></param>
        /// <param name="pTargetInstance"></param>
        public static void ApplyRuleSet(
            Type pRulesAssemblyType, 
            String pRulesManifestResource,
            String pRuleSetName, 
            Type pTargetType, 
            Object pTargetInstance)
        {
            // Load the embedded .rules resource (= pRulesManifestResource parameter)
            // from the specified assembly (= pAssemblyType parameter).
            Assembly vResourceAssembly = Assembly.GetAssembly(pRulesAssemblyType);
            Stream vStream = vResourceAssembly.GetManifestResourceStream(pRulesManifestResource);

            // The RuleSet is serialized and saved in Xml format.
            using (XmlReader vXmlReader = XmlReader.Create(new StreamReader(vStream)))
            {
                // Deserialize the Xml string. The result is a RuleDefinitions object.
                // The RuleDefinitions class is a container for the .rules file that was just loaded.
                // A .rules file can contain one or more RuleSets.
                var vWorkflowMarkupSerializer = new WorkflowMarkupSerializer();
                var vRuleDefinitions = vWorkflowMarkupSerializer.Deserialize(vXmlReader) as RuleDefinitions;
                if (vRuleDefinitions != null)
                {
                    // Extract the wanted RuleSet (= pRuleSetName parameter) from the RuleDefinitions object.
                    if (vRuleDefinitions.RuleSets.Contains(pRuleSetName))
                    {
                        RuleSet vRuleSet = vRuleDefinitions.RuleSets[pRuleSetName];

                        // Next check if the rules within the RuleSet can be a applied to the specified type of object.
                        var vRuleValidation = new RuleValidation(pTargetType, null);
                        if (vRuleSet.Validate(vRuleValidation))
                        {
                            // If the RuleSet is valid for the specified type of object, then execute it against an instance of this class.
                            var vRuleExecution = new RuleExecution(vRuleValidation, pTargetInstance);
                            vRuleSet.Execute(vRuleExecution);
                        }
                    }
                }
            }
        }
    }
}