// using DukandaCore.Application.TodoItems.Commands.CreateTodoItem;
// using DukandaCore.Application.TodoItems.Commands.UpdateTodoItem;
// using DukandaCore.Application.TodoItems.Commands.UpdateTodoItemDetail;
// using DukandaCore.Application.TodoLists.Commands.CreateTodoList;
// using DukandaCore.Domain.Entities;
// using DukandaCore.Domain.Enums;
//
// namespace DukandaCore.Application.FunctionalTests.TodoItems.Commands;
//
// using static Testing;
//
// public class UpdateTodoItemDetailTests : BaseTestFixture
// {
//     [Test]
//     public async Task ShouldRequireValidTodoItemId()
//     {
//         var command = new UpdateTodoItemCommand { Id = Guid.NewGuid(), Title = "New Title" };
//         await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
//     }
//
//     [Test]
//     public async Task ShouldUpdateTodoItem()
//     {
//         var userId = await RunAsDefaultUserAsync();
//
//         var listId = await SendAsync(new CreateTodoListCommand
//         {
//             Title = "New List"
//         });
//
//         var itemId = await SendAsync(new CreateTodoItemCommand
//         {
//             ListId = listId,
//             Title = "New Item"
//         });
//
//         var command = new UpdateTodoItemDetailCommand
//         {
//             Id = itemId,
//             ListId = listId,
//             Note = "This is the note.",
//             Priority = PriorityLevel.High
//         };
//
//         await SendAsync(command);
//
//         var item = await FindAsync<TodoItem>(itemId);
//
//         item.Should().NotBeNull();
//         item!.ListId.Should().Be(command.ListId);
//         item.Note.Should().Be(command.Note);
//         item.Priority.Should().Be(command.Priority);
//         item.LastModifiedById.Should().NotBeNull();
//         item.LastModifiedById.Should().Be(userId);
//         item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
//     }
// }
