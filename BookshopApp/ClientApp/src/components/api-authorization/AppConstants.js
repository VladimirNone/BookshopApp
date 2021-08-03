export const ApplicationName = 'BookshopApp';

const accounttPrefix = "/Account"
const productPrefix = "/Product"
const orderPrefix = "/Order"

const apiPrefix = "/api"
const apiAccountPrefix = `${apiPrefix}${accounttPrefix}`
const apiProductPrefix = `${apiPrefix}${productPrefix}`
const apiOrderPrefix = `${apiPrefix}${orderPrefix}`

export const AppPagePaths = {

    Product:                `${productPrefix}`,
    Cart:                   `${orderPrefix}/Cart`,
    Orders:                 `${orderPrefix}/Orders`,
    Order:                  `${orderPrefix}`,

    Login:                  `${accounttPrefix}/Login`,
    Register:               `${accounttPrefix}/Signup`,
    Profile:                `${accounttPrefix}/Profile`,
    Logout:                 `${accounttPrefix}/Logout`,

}

export const AppApiPaths = {
    Cart:                   `${apiOrderPrefix}/Cart`,
    CartedProductCancel:    `${apiOrderPrefix}/Cart/Cancel`,
    PlaceAnOrder:           `${apiOrderPrefix}/Cart/PlaceAnOrder`,
    AddToCart:              `${apiOrderPrefix}/AddToCart`,
    Orders:                 `${apiOrderPrefix}/Orders`,

    Product:                `${apiProductPrefix}`,
    Products:               `${apiProductPrefix}/Prods`,

    Login:                  `${apiAccountPrefix}/Login`,
    Register:               `${apiAccountPrefix}/Signup`,
    Profile:                `${apiAccountPrefix}/Profile`,
    Logout:                 `${apiAccountPrefix}/Logout`,
    Permision:              `${apiAccountPrefix}/Permision`,
    IsUserAuthenticated:    `${apiAccountPrefix}/Check`,
};
