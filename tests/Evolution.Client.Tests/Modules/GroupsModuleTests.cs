using FluentAssertions;
using Evolution.Client.Core.Http;
using Evolution.Client.Modules;
using NSubstitute;

namespace Evolution.Client.Tests.Modules;

public class GroupsModuleTests
{
    private readonly IHttpService _httpService;
    private readonly GroupsModule _groupsModule;

    public GroupsModuleTests()
    {
        _httpService = Substitute.For<IHttpService>();
        _groupsModule = new GroupsModule(_httpService);
    }

    [Fact]
    public void Constructor_WithNullHttpService_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var action = () => new GroupsModule(null!);
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("httpService");
    }

    [Fact]
    public async Task CreateAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new CreateGroupRequest
        {
            Subject = "Test Group",
            Description = "Test Description",
            Participants = new[] { "5511999999999", "5511888888888" }
        };
        var expectedResponse = new GroupInfo
        {
            Id = "group-123@g.us",
            Subject = "Test Group",
            Owner = "5511999999999@s.whatsapp.net"
        };

        _httpService.PostAsync<CreateGroupRequest, GroupInfo>(
            Arg.Any<string>(),
            Arg.Any<CreateGroupRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.CreateAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<CreateGroupRequest, GroupInfo>(
            $"group/create/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreateAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new CreateGroupRequest
        {
            Subject = "Test Group",
            Participants = new[] { "5511999999999" }
        };

        // Act & Assert
        var action = async () => await _groupsModule.CreateAsync(null!, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task CreateAsync_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _groupsModule.CreateAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("request");
    }

    [Fact]
    public async Task UpdateGroupPictureAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var request = new UpdateGroupPictureRequest
        {
            Image = "base64-image-data"
        };
        var expectedResponse = new GroupOperationResponse
        {
            Success = true,
            Message = "Picture updated successfully"
        };

        _httpService.PostAsync<UpdateGroupPictureRequest, GroupOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdateGroupPictureRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.UpdateGroupPictureAsync(instanceName, groupJid, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdateGroupPictureRequest, GroupOperationResponse>(
            $"group/updateGroupPicture/{instanceName}?groupJid={groupJid}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateGroupSubjectAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var request = new UpdateGroupSubjectRequest
        {
            Subject = "New Group Name"
        };
        var expectedResponse = new GroupOperationResponse
        {
            Success = true,
            Message = "Subject updated successfully"
        };

        _httpService.PostAsync<UpdateGroupSubjectRequest, GroupOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdateGroupSubjectRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.UpdateGroupSubjectAsync(instanceName, groupJid, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdateGroupSubjectRequest, GroupOperationResponse>(
            $"group/updateGroupSubject/{instanceName}?groupJid={groupJid}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FetchInviteCodeAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var expectedResponse = new GroupInviteCodeResponse
        {
            InviteCode = "ABC123DEF456",
            InviteUrl = "https://chat.whatsapp.com/ABC123DEF456"
        };

        _httpService.GetAsync<GroupInviteCodeResponse>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.FetchInviteCodeAsync(instanceName, groupJid);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).GetAsync<GroupInviteCodeResponse>(
            $"group/fetchInviteCode/{instanceName}?groupJid={groupJid}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FetchAllGroupsAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var getParticipants = true;
        var expectedResponse = new List<GroupInfo>
        {
            new GroupInfo
            {
                Id = "group-123@g.us",
                Subject = "Test Group 1",
                Size = 5
            },
            new GroupInfo
            {
                Id = "group-456@g.us",
                Subject = "Test Group 2",
                Size = 3
            }
        };

        _httpService.GetAsync<IEnumerable<GroupInfo>>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.FetchAllGroupsAsync(instanceName, getParticipants);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        await _httpService.Received(1).GetAsync<IEnumerable<GroupInfo>>(
            $"group/fetchAllGroups/{instanceName}?getParticipants={getParticipants}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateParticipantAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var request = new UpdateParticipantRequest
        {
            Action = "add",
            Participants = new[] { "5511999999999", "5511888888888" }
        };
        var expectedResponse = new GroupOperationResponse
        {
            Success = true,
            Message = "Participants updated successfully"
        };

        _httpService.PostAsync<UpdateParticipantRequest, GroupOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdateParticipantRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.UpdateParticipantAsync(instanceName, groupJid, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdateParticipantRequest, GroupOperationResponse>(
            $"group/updateParticipant/{instanceName}?groupJid={groupJid}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task LeaveGroupAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var expectedResponse = new GroupOperationResponse
        {
            Success = true,
            Message = "Left group successfully"
        };

        _httpService.DeleteAsync<GroupOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.LeaveGroupAsync(instanceName, groupJid);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).DeleteAsync<GroupOperationResponse>(
            $"group/leaveGroup/{instanceName}?groupJid={groupJid}",
            Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task UpdateGroupPictureAsync_WithInvalidGroupJid_ShouldThrowArgumentException(string groupJid)
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new UpdateGroupPictureRequest { Image = "base64-data" };

        // Act & Assert
        var action = async () => await _groupsModule.UpdateGroupPictureAsync(instanceName, groupJid, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("JID do grupo é obrigatório*");
    }

    [Fact]
    public async Task FindGroupByInviteCodeAsync_WithNullInviteCode_ShouldThrowArgumentException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _groupsModule.FindGroupByInviteCodeAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Código de convite é obrigatório*");
    }

    [Fact]
    public async Task UpdateGroupDescriptionAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var request = new UpdateGroupDescriptionRequest
        {
            Description = "New group description"
        };
        var expectedResponse = new GroupOperationResponse
        {
            Success = true,
            Message = "Description updated successfully"
        };

        _httpService.PostAsync<UpdateGroupDescriptionRequest, GroupOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdateGroupDescriptionRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.UpdateGroupDescriptionAsync(instanceName, groupJid, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdateGroupDescriptionRequest, GroupOperationResponse>(
            $"group/updateGroupDescription/{instanceName}?groupJid={groupJid}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task RevokeInviteCodeAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var expectedResponse = new GroupInviteCodeResponse
        {
            InviteCode = "NEW123CODE456",
            InviteUrl = "https://chat.whatsapp.com/NEW123CODE456"
        };

        _httpService.PostAsync<object, GroupInviteCodeResponse>(
            Arg.Any<string>(),
            Arg.Any<object>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.RevokeInviteCodeAsync(instanceName, groupJid);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<object, GroupInviteCodeResponse>(
            $"group/revokeInviteCode/{instanceName}?groupJid={groupJid}",
            Arg.Any<object>(),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendGroupInviteAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var request = new SendGroupInviteRequest
        {
            Numbers = new[] { "5511999999999", "5511888888888" },
            Text = "Join our group!"
        };
        var expectedResponse = new GroupOperationResponse
        {
            Success = true,
            Message = "Invites sent successfully"
        };

        _httpService.PostAsync<SendGroupInviteRequest, GroupOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<SendGroupInviteRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.SendGroupInviteAsync(instanceName, groupJid, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendGroupInviteRequest, GroupOperationResponse>(
            $"group/sendInvite/{instanceName}?groupJid={groupJid}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FindGroupByJidAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var getParticipants = true;
        var expectedResponse = new GroupInfo
        {
            Id = groupJid,
            Subject = "Test Group",
            Size = 5,
            Owner = "5511999999999@s.whatsapp.net"
        };

        _httpService.GetAsync<GroupInfo>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.FindGroupByJidAsync(instanceName, groupJid, getParticipants);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).GetAsync<GroupInfo>(
            $"group/findGroupByJid/{instanceName}?groupJid={groupJid}&getParticipants={getParticipants}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FindParticipantsAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var expectedResponse = new List<GroupParticipant>
        {
            new GroupParticipant
            {
                Number = "5511999999999",
                IsAdmin = true,
                Name = "Admin User"
            },
            new GroupParticipant
            {
                Number = "5511888888888",
                IsAdmin = false,
                Name = "Regular User"
            }
        };

        _httpService.GetAsync<IEnumerable<GroupParticipant>>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.FindParticipantsAsync(instanceName, groupJid);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        await _httpService.Received(1).GetAsync<IEnumerable<GroupParticipant>>(
            $"group/findParticipants/{instanceName}?groupJid={groupJid}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateSettingAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var request = new UpdateGroupSettingRequest
        {
            Action = "announcement"
        };
        var expectedResponse = new GroupOperationResponse
        {
            Success = true,
            Message = "Setting updated successfully"
        };

        _httpService.PostAsync<UpdateGroupSettingRequest, GroupOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdateGroupSettingRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.UpdateSettingAsync(instanceName, groupJid, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdateGroupSettingRequest, GroupOperationResponse>(
            $"group/updateSetting/{instanceName}?groupJid={groupJid}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ToggleEphemeralAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var groupJid = "group-123@g.us";
        var request = new ToggleEphemeralRequest
        {
            Expiration = 86400 // 24 hours
        };
        var expectedResponse = new GroupOperationResponse
        {
            Success = true,
            Message = "Ephemeral setting updated successfully"
        };

        _httpService.PostAsync<ToggleEphemeralRequest, GroupOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<ToggleEphemeralRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _groupsModule.ToggleEphemeralAsync(instanceName, groupJid, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<ToggleEphemeralRequest, GroupOperationResponse>(
            $"group/toggleEphemeral/{instanceName}?groupJid={groupJid}",
            request,
            Arg.Any<CancellationToken>());
    }
}
