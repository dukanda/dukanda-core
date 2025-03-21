﻿// using DukandaCore.Application.TodoItems.Commands.CreateTodoItem;
// using DukandaCore.Application.TodoItems.Commands.DeleteTodoItem;
// using DukandaCore.Application.TodoLists.Commands.CreateTodoList;
// using DukandaCore.Domain.Entities;
//
// namespace DukandaCore.Application.FunctionalTests.TodoItems.Commands;
//
// using static Testing;
//
// public class DeleteTodoItemTests : BaseTestFixture
// {
//     [Test]
//     public async Task ShouldRequireValidTodoItemId()
//     {
//         var command = new DeleteTodoItemCommand(Guid.NewGuid());
//
//         await FluentActions.Invoking(() =>
//             SendAsync(command)).Should().ThrowAsync<NotFoundException>();
//     }
//
//     [Test]
//     public async Task ShouldDeleteTodoItem()
//     {
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
//         await SendAsync(new DeleteTodoItemCommand(itemId));
//
//         var item = await FindAsync<TodoItem>(itemId);
//
//         item.Should().BeNull();
//     }
// }
