using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace Mods.FreeFlow.Scripts
{
    internal class Patch
    {
        public static string GetOperandName(in object operand)
        {
            return operand is FieldInfo info ? info.Name : "???";
        }

        public static string GetOperandName(in CodeInstruction instruction)
        {
            return GetOperandName(instruction.operand);
        }

        public static IEnumerable<CodeInstruction> PatchGetOutFlowTranspiler(
                        IEnumerable<CodeInstruction> instructions)
        {
            Debug.Log("Aperion.FreeFlow: PatchGetOutFlowTranspiler");
            var instList = instructions.ToList();
            var wcType = AccessTools.TypeByName("Timberborn.WaterSystem.WaterColumn");
            
            for (var i = 0; i < instList.Count; ++i)
            {
                var instruction = instList[i];
                yield return instruction;
                
                if (instruction.opcode == OpCodes.Ldfld && GetOperandName(instruction) == "MaxWaterfallOutflow")
                {
                     // this is the start of the patch pattern 
                    // ldfld        float32 Timberborn.WaterSystem.WaterSimulatorSettings::MaxWaterfallOutflow
                    // stloc.s      waterfallOutflow
                    Debug.Log($"Aperion.FreeFlow: Start of patch pattern: {instList[i]}, ");
                    yield return instList[++i];
                    yield return new(OpCodes.Ldarg_1);
                    yield return new(OpCodes.Ldfld, wcType.GetField("WaterDepth"));
                    yield return new(OpCodes.Ldloc_S, 18);
                    yield return new(OpCodes.Mul);
                    yield return new(OpCodes.Stloc_S, 18);
                }
            }
        }
    }
}