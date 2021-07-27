export const ApplicationName = 'BookshopApp';

const accountPrefix = '/Account'

export const ApplicationPaths = {

    Login: `${accountPrefix}/Login`,
    Register: `${accountPrefix}/Signup`,
    Profile: `${accountPrefix}/Profile`,
    Logout: `${accountPrefix}/Logout`,
    IsUserAuthenticated: `${accountPrefix}/Check`

};
