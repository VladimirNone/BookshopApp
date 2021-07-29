export const ApplicationName = 'BookshopApp';

const accounttPrefix = "/Account"
const productPrefix = "/Product"
const orderPrefix = "/Order"

const apiPrefix = "/api"
const apiAccountPrefix = `${apiPrefix}${accounttPrefix}`
const apiProductPrefix = `${apiPrefix}${productPrefix}`

export const ApplicationPagePaths = {

    Product:                `${productPrefix}`,
    Cart:                   `${orderPrefix}/Cart`,

    Login:                  `${accounttPrefix}/Login`,
    Register:               `${accounttPrefix}/Signup`,
    Profile:                `${accounttPrefix}/Profile`,
    Logout:                 `${accounttPrefix}/Logout`,

}

export const ApplicationApiPaths = {

    Buy:                    `${apiProductPrefix}/Buy`,
    Product:                `${apiProductPrefix}`,
    Products:               `${apiProductPrefix}/Prods`,

    Login:                  `${apiAccountPrefix}/Login`,
    Register:               `${apiAccountPrefix}/Signup`,
    Profile:                `${apiAccountPrefix}/Profile`,
    Logout:                 `${apiAccountPrefix}/Logout`,
    IsUserAuthenticated:    `${apiAccountPrefix}/Check`,
};