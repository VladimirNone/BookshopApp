import React, { Component } from 'react';
import { MainPage } from './ContentItems/MainPage';
import { Product } from './ContentItems/Product';
import { Cart } from './ContentItems/Cart';
import { Route, Switch } from 'react-router';
import { AppPagePaths } from './Api-authorization/AppConstants';
import { OrderList } from './ContentItems/OrderList';
import { Order } from './ContentItems/Order';
import { ProductManipulator } from './ContentItems/ProductManipulator';

export class Content extends Component {
    render() {

        return (
            <div className="row">
                <div className="col-md border">
                    Категории
			    </div>

                <div className="col-md-7 border">
                    <Switch>
                        <Route exact path='/' component={MainPage} />
                        <Route path={AppPagePaths.ProductManipulator + '/:id'} component={ProductManipulator} />
                        <Route path={AppPagePaths.Product + '/:id'} component={Product} />
                        <Route path={AppPagePaths.Cart} component={Cart} />
                        <Route path={AppPagePaths.Orders} component={OrderList} />
                        <Route path={AppPagePaths.Order + '/:id'} component={Order} />
                    </Switch>
                </div>

                <div className="col-md border">

                </div>
            </div>);
    }
}