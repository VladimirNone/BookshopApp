export const ApplicationName = 'BookshopApp';

const apiPrefix = "/api"
const accountPrefix = `${apiPrefix}/Account`
const productPrefix = `${apiPrefix}/Product`

export const ApplicationPaths = {

    Products:               `${productPrefix}/prods`,

    Login:                  `${accountPrefix}/Login`,
    Register:               `${accountPrefix}/Signup`,
    Profile:                `${accountPrefix}/Profile`,
    Logout:                 `${accountPrefix}/Logout`,
    IsUserAuthenticated:    `${accountPrefix}/Check`

};
