using System;
using System.IO;
using System.Text;
using AipolicyEditor.AIPolicy.Operations;
using System.Windows;

namespace AipolicyEditor.AIPolicy
{
    public static class Operation
    {
        public static IOperation Read(BinaryReader br, int version, int id)
        {
            IOperation op = null;
            switch (id)
            {
                case 0:
                    op = new O_ATTACK_TYPE();
                    break;
                case 1:
                    op = new O_USE_SKILL();
                    break;
                case 2:
                    if (version > 16)
                    {
                        op = new O_TALK_TEXT();
                    }
                    else
                    {
                        op = new O_TALK_TEXT_OLD();
                    }
                    break;
                case 3:
                    op = new O_RESET_HATE_LIST();
                    break;
                case 4:
                    op = new O_RUN_TRIGGER();
                    break;
                case 5:
                    op = new O_STOP_TRIGGER();
                    break;
                case 6:
                    op = new O_ACTIVE_TRIGGER();
                    break;
                case 7:
                    op = new O_CREATE_TIMER();
                    break;
                case 8:
                    op = new O_KILL_TIMER();
                    break;
                case 9:
                    op = new O_FLEE();
                    break;
                case 10:
                    op = new O_SET_HATE_TO_FIRST();
                    break;
                case 11:
                    op = new O_SET_HATE_TO_LAST();
                    break;
                case 12:
                    op = new O_SET_HATE_FIFTY_PERCENT();
                    break;
                case 13:
                    op = new O_SKIP_OPERATION();
                    break;
                case 14:
                    op = new O_ACTIVE_CONTROLLER();
                    break;
                case 15:
                    if (version > 3)
                    {
                        op = new O_SET_GLOBAL();
                    }
                    else
                    {
                        op = new O_SET_GLOBAL_VERSION3();
                    }
                    break;
                case 16:
                    op = new O_REVISE_GLOBAL();
                    break;
                case 17:
                    if (version > 6)
                    {
                        op = new O_SUMMON_MONSTER();
                    }
                    else
                    {
                        op = new O_SUMMON_MONSTER_VERSION6();
                    }
                    break;
                case 18:
                    op = new O_WALK_ALONG();
                    break;
                case 19:
                    if (version > 8)
                    {
                        op = new O_PLAY_ACTION();
                    }
                    else
                    {
                        op = new O_PLAY_ACTION_VERSION8();
                    }
                    break;
                case 20:
                    op = new O_REVISE_HISTORY();
                    break;
                case 21:
                    op = new O_SET_HISTORY();
                    break;
                case 22:
                    op = new O_DELIVER_FACTION_PVP_POINTS();
                    break;
                case 23:
                    op = new O_CALC_VAR();
                    break;
                case 24:
                    op = new O_SUMMON_MONSTER_2();
                    break;
                case 25:
                    op = new O_WALK_ALONG_2();
                    break;
                case 26:
                    op = new O_USE_SKILL_2();
                    break;
                case 27:
                    op = new O_ACTIVE_CONTROLLER_2();
                    break;
                case 28:
                    op = new O_DELIVER_TASK();
                    break;
                case 29:
                    op = new O_SUMMON_MINE();
                    break;
                case 30:
                    op = new O_SUMMON_NPC();
                    break;
                case 31:
                    op = new O_DELIVER_RANDOM_TASK_IN_REGION();
                    break;
                case 32:
                    op = new O_DELIVER_TASK_IN_HATE_LIST();
                    break;
                case 33:
                    op = new O_CLEAR_TOWER_TASK_IN_REGION();
                    break;
                case 34:
                    op = new O_SAVE_PLAYER_COUNT_IN_RADIUS_TO_PARAM();
                    break;
                case 35:
                    op = new O_SAVE_PLAYER_COUNT_IN_REGION_TO_PARAM();
                    break;
                default:
                    MessageBox.Show($"Unknown operation id {id} at pos {br.BaseStream.Position}");
                    break;
            }
            if (br != null)
                op?.Read(br);
            return op;
        }
    }
}
