using Godot;
using AudioSystem;
namespace AudioEmitter;
using System.Collections.Generic;
using System;

// Animation state
public enum animation
{
    WALK,IDLE,JUMP,DOUBLE_JUMP
}

// Data
public class AudioEmitterAgentMovementState
{
    public int      agent_id;
    public float    x,y;    // Float position
    public float    dx,dy;  // Float speed
    public int      standing_material;
    public bool     is_on_ground;
    public float    distance_to_ground;
    public int      frames_since_jump;
    public int      frames_since_double_jump;
    public float    animation_loop_state;
    public animation current_animation;
    public animation next_animation = animation.IDLE;

    // Time frequency of each trigger
    public float    idle_frecuency = 0.7f;
    public float    footsteps_frecuency = 0.5f;
    public float    jump_frecuency = 0.9f;
    public float    double_jump_frecuency = 1.0f;
}

// Logic
public class AudioEmitterAgentMovement 
{
    public AudioEmitterAgentMovementState movement_data = new AudioEmitterAgentMovementState();
    List<Timer> timers = new List<Timer>();

    public void Initialize(int owner_emitter_id)
    {
        //CreateTimer(owner_emitter_id);
        CreateTimer(owner_emitter_id, "Timer_Idle", Idle);              // Index 1
        CreateTimer(owner_emitter_id, "Timer_Footsteps", Walk);         // Index 2
        CreateTimer(owner_emitter_id, "Timer_Footsteps", Jump);         // Index 3
        CreateTimer(owner_emitter_id, "Timer_Footsteps", DoubleJump);   // Index 4
    }

    // Animation update
    public void Update()
    {
        if(movement_data.current_animation != movement_data.next_animation)
        {
            switch(movement_data.next_animation)
            {
                case animation.IDLE:
                    movement_data.current_animation = animation.IDLE;
                    StopAllTimers();
                    StartTimer(timers[0], movement_data.idle_frecuency);
                    Idle();
                    break;
                case animation.WALK:
                    movement_data.current_animation = animation.WALK;
                    StopAllTimers();
                    StartTimer(timers[1], movement_data.footsteps_frecuency);
                    break;
                case animation.JUMP:
                    movement_data.current_animation = animation.JUMP;
                    StopAllTimers();
                    StartTimer(timers[2], movement_data.jump_frecuency);
                    break;
                case animation.DOUBLE_JUMP:
                    movement_data.current_animation = animation.DOUBLE_JUMP;
                    StopAllTimers();
                    StartTimer(timers[3], movement_data.double_jump_frecuency);
                    break;
            }
        }
    }

    public void  Walk()
    {
        GD.Print("audio_emitter : STATE -> 'Walk'");
        SoundApi.audio_playback_manager.PlayAudioInSequence(movement_data.agent_id, "Walk");
    }

    public void  Idle()
    {
        GD.Print("audio_emitter : STATE -> 'Idle'");
        SoundApi.audio_playback_manager.PlayAudioInSequence(movement_data.agent_id, "Idle");
    }

    public void  Jump()
    {
        GD.Print("audio_emitter : STATE -> 'Jump'");
        SoundApi.audio_playback_manager.PlayAudioInSequence(movement_data.agent_id, "Jump");
    }

    public void  DoubleJump()
    {
        GD.Print("audio_emitter : STATE -> 'DoubleJump'");
        SoundApi.audio_playback_manager.PlayAudioInSequence(movement_data.agent_id, "DoubleJump");
    }

    public void CreateTimer(int game_object_id, string name, Action function_to_call)
    {
        Timer new_timer;
        new_timer = new Timer();                                                           // Create timer for footsteps
        SoundApi.audio_emitter_manager.audio_emitters[game_object_id].AddChild(new_timer); // Add it to object
        new_timer.Timeout += () => function_to_call();                                     // Connect it to footstep trigger
        timers.Add(new_timer);                                                             // Add it to timer's list
        new_timer.Name = name;
        
    }

    public void StartTimer(Timer timer_to_start, float time)
    {
        if(timer_to_start.IsStopped())
        {
            timer_to_start.Start(time);
        }
    }

    public void StopAllTimers()
    {
        foreach(Timer timer in timers)
        {
            timer.Stop();
        }
    }
}