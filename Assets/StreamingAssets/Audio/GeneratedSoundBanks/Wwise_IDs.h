/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMBIENT_COLLAPSE = 941448663U;
        static const AkUniqueID AMBIENT_DISTANTIMPACTS = 269417926U;
        static const AkUniqueID AMBIENT_LAVA = 4275106484U;
        static const AkUniqueID AMBIENT_WIND = 1410046624U;
        static const AkUniqueID EGG_LEVITATE = 1242907925U;
        static const AkUniqueID EGG_PICKUP = 1485126323U;
        static const AkUniqueID EGG_SHIELDACTIVATE = 16747207U;
        static const AkUniqueID EGG_SHIELDDESTROY = 1126770210U;
        static const AkUniqueID EGG_SOUL_IMPACT = 3220447791U;
        static const AkUniqueID ENEMY_DEATH = 1205999388U;
        static const AkUniqueID ENEMY_FOOTSTEP_ADULT = 3521032401U;
        static const AkUniqueID ENEMY_FOOTSTEP_BABY = 1530017751U;
        static const AkUniqueID ENEMY_SHOOT_BASIC = 3691532132U;
        static const AkUniqueID ENEMY_SPAWN_ADULT = 1388907814U;
        static const AkUniqueID ENEMY_SPAWN_BABY = 1815360318U;
        static const AkUniqueID ENEMY_TAKEDAMAGE = 842135496U;
        static const AkUniqueID LUMMING_ABILITY_ELECTRIC_IMPACT = 4213470894U;
        static const AkUniqueID LUMMING_ABILITY_ELECTRIC_START = 768425324U;
        static const AkUniqueID LUMMING_ABILITY_FIRE_IMPACT = 2208495489U;
        static const AkUniqueID LUMMING_ABILITY_FIRE_START = 1749973457U;
        static const AkUniqueID LUMMING_HEAL = 2851111675U;
        static const AkUniqueID LUMMING_IDLE_ELECTRICINTERACTION = 1703828721U;
        static const AkUniqueID LUMMING_IDLE_FIREINTERACTION = 1512820010U;
        static const AkUniqueID LUMMING_LEVITATE = 3986156551U;
        static const AkUniqueID MUSIC_GAMEPLAY_COLLAPSE = 3966329825U;
        static const AkUniqueID MUSIC_GAMEPLAY_REST = 3112455934U;
        static const AkUniqueID MUSIC_GAMEPLAY_STAGE1 = 4066633163U;
        static const AkUniqueID MUSIC_GAMEPLAY_STAGE2 = 4066633160U;
        static const AkUniqueID MUSIC_GAMEPLAY_STAGE3 = 4066633161U;
        static const AkUniqueID MUSIC_MENU = 1598298728U;
        static const AkUniqueID MUSIC_STINGER_DEFEAT = 1479913785U;
        static const AkUniqueID MUSIC_STINGER_VICTORY = 4254684058U;
        static const AkUniqueID PLAYER_BURN = 3890490206U;
        static const AkUniqueID PLAYER_FOOTSTEP = 2453392179U;
        static const AkUniqueID PLAYER_JUMP = 1305133589U;
        static const AkUniqueID PLAYER_SHOOT = 4004702906U;
        static const AkUniqueID PLAYER_SHOOTCHARGE_ELECTRIC = 3840296224U;
        static const AkUniqueID PLAYER_TAKEDAMAGE_GENERIC = 3727341601U;
        static const AkUniqueID PROJECTILE_HIT_BASICPROJECTILE = 950925848U;
        static const AkUniqueID PROJECTILE_HIT_ELECTRICPROJECTILE = 3600532549U;
        static const AkUniqueID PROJECTILE_SOUL_DEPLOY = 2994932292U;
        static const AkUniqueID UI_BUTTON_NORMAL = 3345914708U;
        static const AkUniqueID UI_BUTTON_SPECIAL = 3115620430U;
        static const AkUniqueID UI_SLIDER = 3987036369U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace PLAYER_BURN
        {
            static const AkUniqueID GROUP = 3890490206U;

            namespace STATE
            {
                static const AkUniqueID BURNING = 3518279182U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID NOT_BURNING = 4104232280U;
            } // namespace STATE
        } // namespace PLAYER_BURN

    } // namespace STATES

    namespace SWITCHES
    {
        namespace FOOTSTEP_SURFACE
        {
            static const AkUniqueID GROUP = 1833605183U;

            namespace SWITCH
            {
                static const AkUniqueID LAVA = 540301611U;
                static const AkUniqueID STONE = 1216965916U;
            } // namespace SWITCH
        } // namespace FOOTSTEP_SURFACE

        namespace PLAYER_SHOOT_TYPE
        {
            static const AkUniqueID GROUP = 3124051321U;

            namespace SWITCH
            {
                static const AkUniqueID BASIC = 3340296461U;
                static const AkUniqueID ELECTRIC = 3250089732U;
            } // namespace SWITCH
        } // namespace PLAYER_SHOOT_TYPE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID CROSSFADE = 1369808713U;
        static const AkUniqueID PITCH_SLIDER = 3748721091U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID LUMMINS_SOUNDBANK = 4217922608U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID UI = 1551306167U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
