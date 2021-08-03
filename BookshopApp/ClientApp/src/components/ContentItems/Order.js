import React, { Component, Fragment } from 'react';
import { Pagination } from '../SubComponents/Pagination';
import { AppApiPaths } from '../Api-authorization/AppConstants';
import { OrderedProductCard } from '../SubComponents/OrderedProductCard';
import { OrderCard } from '../SubComponents/OrderCard';

export class Order extends Component {
    constructor(props) {
        super(props);

        this.state = {
            orderId: parseInt(props.match.params.id),
            order: null,
            page: 1,
            pageIsLast: false,
        }
    }

    componentDidMount() {
        this.getProductsFromServer();
    }

    async getProductsFromServer() {
        //The server pagination starts from zero
        let response = await fetch(AppApiPaths.Order + '/' + this.state.orderId + '/' + (this.state.page - 1), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok)
            console.log("error");
        else {
            const answer = await response.json();
            this.setState({ order: answer.order, pageIsLast: answer.pageIsLast });
        }
    }

    render() {
        const order = this.state.order;
        if (order == null)
            return (<div />);

        return (
            <Fragment>
                {<OrderCard order={order} />}
                <div className="row mt-3 ">
                    <div className="col">
                        <h3>Список продуктов из заказа: </h3>
                    </div>
                </div>
                {order.orderedProducts.map((prod, i) => <OrderedProductCard key={i} product={prod.product} countOfDeferredProduct={prod.count} updateCart={() => this.setState({ dataWasUpdated: false })} />)}
                <Pagination curPage={this.state.page} pageIsLast={this.state.pageIsLast} changePage={(newPage) => this.setState({ page: newPage, dataWasUpdated: false })} />
            </Fragment>);
    }
}