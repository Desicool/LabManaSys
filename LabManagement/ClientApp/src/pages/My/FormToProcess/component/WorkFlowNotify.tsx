import React from 'react';
import { Card, List } from 'antd';
import styles from '../style.less';
import { IWorkFlow } from '@/models/entity';
import moment from 'moment';
import { INotifyResult, history } from 'umi';

const ListContent = ({
    data: { uname, startTime },
}: {
    data: IWorkFlow;
}) => (
        <div className={styles.listContent}>
            <div className={styles.listContentItem}>
                <span>Owner</span>
                <p>{uname}</p>
            </div>
            <div className={styles.listContentItem}>
                <span>开始时间</span>
                <p>{moment(startTime).format('YYYY-MM-DD HH:mm')}</p>
            </div>
        </div>
    );
export const NotifyWorkFlowComponent = ({ notify }: {
    notify?: INotifyResult
}) => {
    return (
        <List
            size="large"
            rowKey="id"
            pagination={{
                showSizeChanger: true,
                showQuickJumper: true,
                pageSize: 5,
                total: notify?.wf.length,
            }}
            dataSource={notify?.wf}
            renderItem={(item) => (
                <List.Item
                    actions={[
                        <a
                            key="edit"
                            onClick={(e) => {
                                e.preventDefault();
                                history.push('/my/workflow/' + item.id?.toString(), {
                                    workflow: item
                                });
                            }}
                        >
                            查看
                    </a>,
                    ]}
                >
                    <List.Item.Meta
                        title={item.description}
                    />
                    <ListContent data={item} />
                </List.Item>
            )}
        />
    )
}