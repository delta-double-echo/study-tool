using StudyTool.Core.Models;

namespace StudyTool.Data;

public static class SeedData
{
    public static async Task InitialiseAsync(AppDbContext db)
    {
        if (db.Groups.Any()) return;

        var gettingStarted = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Getting Started",
            CreatedBy = "owner",
            CreatedAt = DateTime.UtcNow
        };

        var usingTheLibrary = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Using the Library",
            CreatedBy = "owner",
            CreatedAt = DateTime.UtcNow
        };

        var quizTips = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Quiz Tips",
            CreatedBy = "owner",
            CreatedAt = DateTime.UtcNow
        };

        db.Groups.AddRange(gettingStarted, usingTheLibrary, quizTips);

        db.Cards.AddRange(

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = gettingStarted.Id,
                Title = "Welcome to StudyTool — what is this app?",
                Content = "StudyTool is a personal interview preparation tool. You create study cards organised into groups, then quiz yourself using cue card mode or AI-generated multiple choice questions. Use it to build confidence before interviews and track which topics come up most in the real thing.",
                CreatedBy = "owner",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = gettingStarted.Id,
                Title = "How do I create my first card?",
                Content = "Click '+ Add card' in the top right of the Library page. Fill in the group (e.g. 'System Design'), a title which is the question you want to practise, and the content which is your ideal answer. Optionally attach an image such as a diagram. Hit Save and the card appears in your library instantly.",
                CreatedBy = "owner",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = gettingStarted.Id,
                Title = "What are groups and how should I organise them?",
                Content = "Groups are topic areas that organise your cards — for example 'System Design', 'Algorithms', 'Behavioural', or 'SQL'. Keep group names broad enough to contain many cards but specific enough to be meaningful. You can filter the library and quiz by group, so good organisation pays off when you want to focus on a weak area.",
                CreatedBy = "owner",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = usingTheLibrary.Id,
                Title = "How does search work?",
                Content = "The search bar filters cards in real time across both the title (question) and content (answer) fields. Use it to quickly find a specific topic. Combine it with the group filter in the sidebar to narrow results further — for example searching 'cache' within the System Design group only.",
                CreatedBy = "owner",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = usingTheLibrary.Id,
                Title = "What is the improve flag and when should I use it?",
                Content = "The flag icon on each card marks it as needing improvement. Use it when you feel your answer is incomplete, unclear, or outdated. You can then filter the library to show flagged cards only and work through them in a dedicated review session. Remove the flag once you are happy with the updated content.",
                CreatedBy = "owner",
                FlaggedForImprovement = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = usingTheLibrary.Id,
                Title = "What does the tally counter track?",
                Content = "The tally is a manual counter you increment each time a question comes up in a real interview or technical test. Tap the lightbulb icon on any card to add one. Over time this shows you which questions interviewers actually ask most, helping you prioritise what to study. It is not automated — you control it.",
                CreatedBy = "owner",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = quizTips.Id,
                Title = "What is cue card mode and how do I use it?",
                Content = "Cue card mode shows you the question and waits for you to recall the answer from memory. When ready, click Reveal to see your answer. Be honest with yourself — the goal is active recall, not passive reading. Use the Previous and Next buttons to move through your selected cards. Shuffle is recommended for realistic practice.",
                CreatedBy = "owner",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = quizTips.Id,
                Title = "What is Q&A mode and how are the answers generated?",
                Content = "Q&A mode presents each question with four answer choices — one correct and three plausible alternatives generated by AI based on your answer content. Select your answer to immediately see if you were right and read an explanation. This mode tests recognition rather than recall and is useful for checking whether you can spot the right answer under pressure.",
                CreatedBy = "owner",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            new Card
            {
                Id = Guid.NewGuid(),
                GroupId = quizTips.Id,
                Title = "How should I select groups for a quiz session?",
                Content = "On the Quiz page, check the groups you want to include. For broad revision select all groups. For targeted practice before a specific interview, select only the relevant groups — for example just Behavioural if the next round is an HR interview. The card count updates as you change your selection so you know how long the session will take.",
                CreatedBy = "owner",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        await db.SaveChangesAsync();
    }
}