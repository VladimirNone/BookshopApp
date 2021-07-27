export const ApplicationName = 'BookshopApp';

const apiPrefix = "api"
const accountPrefix = 'Account'

export const ApplicationPaths = {

    Login:                  `/${apiPrefix}/${accountPrefix}/Login`,
    Register:               `/${apiPrefix}/${accountPrefix}/Signup`,
    Profile:                `/${apiPrefix}/${accountPrefix}/Profile`,
    Logout:                 `/${apiPrefix}/${accountPrefix}/Logout`,
    IsUserAuthenticated:    `/${apiPrefix}/${accountPrefix}/Check`

};
