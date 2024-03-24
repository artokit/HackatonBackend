using EducationService.Controllers;
using EducationService.Models;
using EducationService.Services;
using Microsoft.AspNetCore.SignalR;

namespace EducationService.Hubs;

public class VersusHub : Hub
{
    private readonly TaskService taskService;

    public VersusHub(TaskService taskService)
    {
        this.taskService = taskService;
    }

public async Task CreateAndAddToGroup(string user1connectionId, string user2connectionId)
    {
        var newGroupName = Guid.NewGuid().ToString();
        await Groups.AddToGroupAsync(user1connectionId, newGroupName);
        await Groups.AddToGroupAsync(user2connectionId, newGroupName);

        await Clients.Groups(newGroupName).SendAsync("NewGroupCreated", newGroupName);
    }

    public async Task GetTask(string groupName)
    {
        var task = taskService.Random();
        await Clients.Groups(groupName).SendAsync("receive", task);
    }
    //TODO: Сделать версус с убавлением рейтинга
    public async Task CompareAnswersAndRemoveUsers(string groupName, string UserId, string answer, TaskCase taskCase)
    {
        if (answer == taskCase.RightAnswer)
        {
            await Clients.Groups(groupName).SendAsync("receive", $"{UserId} победил!");
            await taskService.Solve(taskCase.Id, answer, Convert.ToInt32(UserId));
        }
        else
        {
            await Clients.Client(UserId).SendAsync("receive", "Wrong answer");
        }
    }

    public async Task RemoveGroup(string userId1, string userId2, string groupName)
    {
        await Groups.RemoveFromGroupAsync(userId1, groupName);
        await Groups.RemoveFromGroupAsync(userId2, groupName);
    }
}
