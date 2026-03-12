using System.Numerics;
using Raylib_cs;

namespace ConsoleApp1.GameCode;

public class Player : LivingEntity
{
    public enum AnimationState
    {
        BreathingIdle,
        FightStanceIdle,
        Walking,
        Running
    }

    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    private Dictionary<AnimationState, Dictionary<Direction, Animation>> _animations = new();
    private AnimationState _currentState = AnimationState.BreathingIdle;
    private Direction _currentDirection = Direction.South;
    private int _breathingLoops = 0;

    public float CurrentSpeedMultiplier { get; set; } = 1.0f;

    public Player() : base()
    {
        LoadAnimations();
    }

    public Player(Vector2 position, Vector2 size, float movementSpeed = 100f, float rotation = 0f) 
        : base(position, size, movementSpeed, rotation)
    {
        LoadAnimations();
    }

    private void LoadAnimations()
    {
        // Define animation paths based on metadata.json structure
        // Breathing Idle (only south in metadata.json snippet, let's assume south for others or fallback)
        _animations[AnimationState.BreathingIdle] = new Dictionary<Direction, Animation>
        {
            { Direction.South, new Animation(new[] {
                "animations/breathing-idle/south/frame_000.png",
                "animations/breathing-idle/south/frame_001.png",
                "animations/breathing-idle/south/frame_002.png",
                "animations/breathing-idle/south/frame_003.png"
            }, 8f) }
        };

        // Fight Stance Idle
        _animations[AnimationState.FightStanceIdle] = new Dictionary<Direction, Animation>
        {
            { Direction.South, new Animation(new[] {
                "animations/fight-stance-idle-8-frames/south/frame_000.png",
                "animations/fight-stance-idle-8-frames/south/frame_001.png",
                "animations/fight-stance-idle-8-frames/south/frame_002.png",
                "animations/fight-stance-idle-8-frames/south/frame_003.png",
                "animations/fight-stance-idle-8-frames/south/frame_004.png",
                "animations/fight-stance-idle-8-frames/south/frame_005.png",
                "animations/fight-stance-idle-8-frames/south/frame_006.png",
                "animations/fight-stance-idle-8-frames/south/frame_007.png"
            }, 10f, false) }
        };

        // Walking
        var walkingAnimations = new Dictionary<Direction, Animation>();
        string[] dirs = { "north", "south", "east", "west" };
        foreach (var d in dirs)
        {
            var dir = Enum.Parse<Direction>(d, true);
            walkingAnimations[dir] = new Animation(new[] {
                $"animations/walking-6-frames/{d}/frame_000.png",
                $"animations/walking-6-frames/{d}/frame_001.png",
                $"animations/walking-6-frames/{d}/frame_002.png",
                $"animations/walking-6-frames/{d}/frame_003.png",
                $"animations/walking-6-frames/{d}/frame_004.png",
                $"animations/walking-6-frames/{d}/frame_005.png"
            }, 10f);
        }
        _animations[AnimationState.Walking] = walkingAnimations;

        // Running
        var runningAnimations = new Dictionary<Direction, Animation>();
        foreach (var d in dirs)
        {
            var dir = Enum.Parse<Direction>(d, true);
            runningAnimations[dir] = new Animation(new[] {
                $"animations/running-6-frames/{d}/frame_000.png",
                $"animations/running-6-frames/{d}/frame_001.png",
                $"animations/running-6-frames/{d}/frame_002.png",
                $"animations/running-6-frames/{d}/frame_003.png",
                $"animations/running-6-frames/{d}/frame_004.png",
                $"animations/running-6-frames/{d}/frame_005.png"
            }, 12f);
        }
        _animations[AnimationState.Running] = runningAnimations;
    }

    public override void Update(float deltaTime)
    {
        // Simple WASD movement handled via PlayerController
        PlayerController.UpdateInput();
        PlayerController.HandleMovement(this, deltaTime);

        UpdateAnimationState(deltaTime);
    }

    private void UpdateAnimationState(float deltaTime)
    {
        Vector2 move = PlayerController.MovementVector;
        bool isMoving = move != Vector2.Zero;

        if (isMoving)
        {
            // Update direction: prioritize horizontal movement
            if (move.X != 0)
            {
                _currentDirection = move.X > 0 ? Direction.East : Direction.West;
            }
            else if (move.Y != 0)
            {
                _currentDirection = move.Y > 0 ? Direction.South : Direction.North;
            }

            // Update state
            AnimationState newState = PlayerController.IsSprinting ? AnimationState.Running : AnimationState.Walking;
            if (_currentState != newState)
            {
                _currentState = newState;
                ResetAnimation();
            }
        }
        else
        {
            // Idle logic: 3 loops of breathing, then 1 fight stance
            if (_currentState == AnimationState.Walking || _currentState == AnimationState.Running)
            {
                _currentState = AnimationState.BreathingIdle;
                _breathingLoops = 0;
                ResetAnimation();
            }

            var currentAnim = GetCurrentAnimation();
            if (_currentState == AnimationState.BreathingIdle)
            {
                if (currentAnim.LoopsCompleted >= 3)
                {
                    _currentState = AnimationState.FightStanceIdle;
                    ResetAnimation();
                }
            }
            else if (_currentState == AnimationState.FightStanceIdle)
            {
                if (currentAnim.IsFinished)
                {
                    _currentState = AnimationState.BreathingIdle;
                    _breathingLoops = 0;
                    ResetAnimation();
                }
            }
        }

        GetCurrentAnimation().Update(deltaTime);
    }

    private Animation GetCurrentAnimation()
    {
        var stateAnims = _animations[_currentState];
        if (stateAnims.TryGetValue(_currentDirection, out var anim))
        {
            return anim;
        }
        // Fallback to South if direction not found (e.g. idle only has South)
        return stateAnims[Direction.South];
    }

    private void ResetAnimation()
    {
        GetCurrentAnimation().Reset();
    }

    public override void Render()
    {
        GetCurrentAnimation().Draw(Position, new Vector2(Size.X * 2, Size.Y), Rotation, Color.White);
    }

    public void RenderAt(Vector2 screenPosition)
    {
        GetCurrentAnimation().Draw(screenPosition, new Vector2(Size.X * 2, Size.Y), Rotation, Color.White);
    }
}

public static class PlayerController
{
    public static float Acceleration { get; set; } = 2.0f; // Speed multiplier increase per second
    public static float MaxSprintMultiplier { get; set; } = 4.0f;

    public static Vector2 MovementVector => _movementVector;
    public static bool IsSprinting => _isSprinting;

    private static Vector2 _movementVector = Vector2.Zero;
    private static bool _isSprinting = false;

    public static class CursorController
    {
        public static void Render()
        {
            Vector2 mousePos = Raylib.GetMousePosition();
            Raylib.DrawCircleV(mousePos, 5, Color.Black);
        }
    }

    public static void UpdateInput()
    {
        _movementVector = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) _movementVector.Y -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.S)) _movementVector.Y += 1;
        if (Raylib.IsKeyDown(KeyboardKey.A)) _movementVector.X -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.D)) _movementVector.X += 1;

        _isSprinting = Raylib.IsKeyDown(KeyboardKey.LeftShift) || Raylib.IsKeyDown(KeyboardKey.RightShift);
    }

    public static void HandleMovement(Player player, float deltaTime)
    {
        // Handle sprinting
        if (_isSprinting)
        {
            player.CurrentSpeedMultiplier += Acceleration * deltaTime;
        }
        else
        {
            // Gradually slow down if shift is released
            player.CurrentSpeedMultiplier -= Acceleration * deltaTime;
        }

        // Clamp speed multiplier between 1.0 and MaxSprintMultiplier
        player.CurrentSpeedMultiplier = Math.Clamp(player.CurrentSpeedMultiplier, 1.0f, MaxSprintMultiplier);

        if (_movementVector != Vector2.Zero)
        {
            // Normalize movement vector to avoid faster diagonal movement
            Vector2 movement = Vector2.Normalize(_movementVector);
            player.Position += movement * (player.MovementSpeed * player.CurrentSpeedMultiplier) * deltaTime;
        }
    }
}
