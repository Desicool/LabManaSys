import React, { FC, useEffect } from 'react';
import {
  Card,
  Input,
  List,
  Radio,
} from 'antd';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect, Dispatch, history } from 'umi';
import moment from 'moment';
import { WorkFlowListStateType } from './model';
import styles from './style.less';
import { IWorkFlow } from '@/models/entity';

const RadioButton = Radio.Button;
const RadioGroup = Radio.Group;
const { Search } = Input;

interface WorkFlowListProps {
  workFlowList: WorkFlowListStateType;
  dispatch: Dispatch;
  loading: boolean;
}

const ListContent = ({
  data: { uid,startTime },
}: {
  data: IWorkFlow;
}) => (
  <div className={styles.listContent}>
    <div className={styles.listContentItem}>
      <span>Owner</span>
      <p>{uid}</p>
    </div>
    <div className={styles.listContentItem}>
      <span>开始时间</span>
      <p>{moment(startTime).format('YYYY-MM-DD HH:mm')}</p>
    </div>
  </div>
);

export const WorkFlowList: FC<WorkFlowListProps> = (props) => {
  const {
    loading,
    dispatch,
    workFlowList: { list },
  } = props;
  useEffect(() => {
    dispatch({
      type: 'workFlowList/fetch',
      payload: {
        count: 5,
      },
    });
  }, [1]);

  const paginationProps = {
    showSizeChanger: true,
    showQuickJumper: true,
    pageSize: 5,
    total: 50,
  };


  const extraContent = (
    <div className={styles.extraContent}>
      <RadioGroup defaultValue="all">
        <RadioButton value="all">全部</RadioButton>
        <RadioButton value="progress">进行中</RadioButton>
        <RadioButton value="waiting">等待中</RadioButton>
      </RadioGroup>
      <Search className={styles.extraContentSearch} placeholder="请输入" onSearch={() => ({})} />
    </div>
  );

  return (
    <div>
      <PageHeaderWrapper>
        <div className={styles.standardList}>
          <Card
            className={styles.listCard}
            bordered={false}
            title="基本列表"
            style={{ marginTop: 24 }}
            bodyStyle={{ padding: '0 32px 40px 32px' }}
            extra={extraContent}
          >
            <List
              size="large"
              rowKey="id"
              loading={loading}
              pagination={paginationProps}
              dataSource={list}
              renderItem={(item) => (
                <List.Item
                  actions={[
                    <a
                      key="edit"
                      onClick={(e) => {
                        e.preventDefault();
                        history.push('/my/workflow/' + item.id?.toString(),{
                          workflow : item
                        });
                      }}
                    >
                      查看
                    </a>,
                  ]}
                >
                  <List.Item.Meta
                    title={item.id?.toString()}
                  />
                  <ListContent data={item} />
                </List.Item>
              )}
            />
          </Card>
        </div>
      </PageHeaderWrapper>
    </div>
  );
};

export default connect(
  ({
    workFlowList,
    loading,
  }: {
    workFlowList: WorkFlowListStateType;
    loading: {
      models: { [key: string]: boolean };
    };
  }) => ({
    workFlowList,
    loading: loading.models.workFlowList,
  }),
)(WorkFlowList);
