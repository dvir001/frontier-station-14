## ``playtime_unlock`` command

cmd-playtime_unlock-desc = Unlock the playtime requirement for specific jobs. 
cmd-playtime_unlock-help = Usage: {$command} [user name] [trackers...]
    This command unlocks the playtime requirements for specific jobs for a user.
cmd-playtime_unlock-arg-user = [user name]
cmd-playtime_unlock-arg-job = [job id]
cmd-playtime_unlock-error-args = Expected zero or one arguments
cmd-playtime_unlock-error-job = Expected a valid JobPrototype for the second argument, but got {$invalidJob}.
cmd-playtime_unlock-error-no-requirements = No CharacterPlaytimeRequirements or CharacterDepartmentTime requirements found.
