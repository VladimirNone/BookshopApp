import React, { Component, Fragment } from 'react';
import { Pagination } from '../SubComponents/Pagination';
import { AppApiPaths, AppPagePaths } from '../Api-authorization/AppConstants';
import { OrderCard } from '../SubComponents/OrderCard';
import authService from '../Api-authorization/AuthorizeService';

export class OrderList extends Component {
    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            orders: [],
            pageIsLast: false,
            dataWasUpdated: true,
            access: false,
            orderSourse: AppApiPaths.Orders,
        }
    }

    componentDidMount() {
        this.getOrdersFromServer();
        this.getPermission();
    }

    componentDidUpdate() {
        if (!this.state.dataWasUpdated)
            this.getOrdersFromServer();
    }

    async getPermission() {
        authService.checkPermission((res) => this.setState({ access: res.access }), (error) => console.error("OrderList. getPermission()"));
    }

    async getOrdersFromServer() {
        //The server pagination starts from zero
        let response = await fetch(this.state.orderSourse + '/' + (this.state.page - 1), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok)
            console.error("OrderList. getOrdersFromServer()");
        else {
            const answer = await response.json();
            this.setState({ orders: answer.orders, pageIsLast: answer.pageIsLast, dataWasUpdated: true });
        }
    }

    render() {
        const orders = this.state.orders;
        if (orders == null)
            return (<div />);

        let hiddenButtons = (
            <div>
                <div className="d-flex justify-content-center mt-3 ">
                    <div className="mr-2">
                        <button type="button" className="btn btn-light" onClick={() => this.setState({ orderSourse: AppApiPaths.Orders, page: 1, dataWasUpdated: false })}>Мои заказы</button>
                    </div>
                    <div className="ml-2">
                        <button type="button" className="btn btn-light" onClick={() => this.setState({ orderSourse: AppApiPaths.GlobalOrders, page: 1, dataWasUpdated: false })}>Все заказы</button>
                    </div>
                </div>
            </div>);

        if (orders.length === 0)
            return (
                <Fragment>
                    {this.state.access && hiddenButtons}
                    <div className="row mt-3 ">
                        <div className="col">
                            <h3>У вас нет заказов</h3>
                        </div>
                    </div>
                </Fragment>);

        return (
            <Fragment>
                {this.state.access && hiddenButtons}
                <div className="row no-gutters mt-3 ">
                    <div className="col">
                        <h3>Заказы:</h3>
                    </div>
                </div>
                {orders.map((order, i) => <OrderCard key={i} order={order} admin={this.state.access }/>)}
                <Pagination curPage={this.state.page} pageIsLast={this.state.pageIsLast} changePage={(newPage) => this.setState({ page: newPage, dataWasUpdated: false })} />
            </Fragment>);
    }
}