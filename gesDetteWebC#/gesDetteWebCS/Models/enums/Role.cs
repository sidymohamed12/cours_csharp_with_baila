namespace gesDetteWebCS.Models.enums
{
    public enum Role
    {
        admin,
        boutiquier,
        client

        // public static Role getRole(String value) {
        //     return java.util.Arrays.stream(Role.values())
        //             .filter(role -> role.name().equalsIgnoreCase(value))
        //             .findFirst()
        //             .orElse(null);
        // }

        // public static Role getRoleById(int id) {
        //     return java.util.Arrays.stream(Role.values())
        //             .filter(role -> role.getId() == id)
        //             .findFirst()
        //             .orElse(null);
        // }
    }
}